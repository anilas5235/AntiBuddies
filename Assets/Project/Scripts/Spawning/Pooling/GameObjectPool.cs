using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Project.Scripts.Spawning.Pooling
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialObjCount = 10;
        [SerializeField] private int currentTotal;
        [SerializeField] private int currentInPool;
        private readonly Stack<IPoolable> _pool = new();
        
        public void Init()
        {
            FillPoolTo(initialObjCount);
        }

        public IPoolable GetObject()
        {
            IPoolable obj = IsEmpty ? CreateNewInstance() : _pool.Pop();
            currentInPool = _pool.Count;
            obj.Activate();
            return obj;
        }

        private IPoolable CreateNewInstance()
        {
            GameObject obj = Object.Instantiate(prefab);
            currentTotal++;
            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable.Init(this);
            return poolable;
        }
        
        public void AddToPool(IPoolable obj){
            obj.Reset();
            obj.Deactivate();
            _pool.Push(obj);
            currentInPool = _pool.Count;
        }

        private bool IsEmpty => _pool.Count == 0;

        private void FillPoolTo(int count)
        {
            while (_pool.Count < count)
            {
                AddToPool(CreateNewInstance());
            }
        }
    }
}
