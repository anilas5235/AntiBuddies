using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Project.Scripts.Spawning.Pooling
{
    [Serializable]
    public class GameObjectPool<TData>
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialObjCount = 10;
        [SerializeField] private int currentTotal;
        [SerializeField] private int currentInPool;
        private readonly Stack<IPoolable<TData>> _pool = new();
        
        public void Init()
        {
            FillPoolTo(initialObjCount);
        }

        public IPoolable<TData> GetObject()
        {
            IPoolable<TData> obj = IsEmpty ? CreateNewInstance() : _pool.Pop();
            currentInPool = _pool.Count;
            return obj;
        }

        private IPoolable<TData> CreateNewInstance()
        {
            GameObject obj = Object.Instantiate(prefab);
            currentTotal++;
            IPoolable<TData> poolable = obj.GetComponent<IPoolable<TData>>();
            poolable.Init(this);
            return poolable;
        }
        
        public void AddToPool(IPoolable<TData> obj){
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
