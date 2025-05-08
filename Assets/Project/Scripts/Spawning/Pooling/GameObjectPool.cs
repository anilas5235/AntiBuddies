using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.Spawning.Pooling
{
    [Serializable]
    public class GameObjectPool<TData>
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialObjCount = 10;
        [SerializeField] private int objCount;
        private readonly Stack<IPoolable<TData>> _pool = new();
        
        private void Init()
        {
            FillPoolTo(initialObjCount);
        }

        public IPoolable<TData> GetObject()
        {
            IPoolable<TData> obj = IsEmpty ? CreateNewInstance() : _pool.Pop();
            return obj;
        }

        private IPoolable<TData> CreateNewInstance()
        {
            GameObject obj = Object.Instantiate(prefab);
            objCount++;
            IPoolable<TData> poolable = obj.GetComponent<IPoolable<TData>>();
            poolable.Init(this);
            return poolable;
        }
        
        public void AddToPool(IPoolable<TData> obj){
            obj.Reset();
            obj.Deactivate();
            _pool.Push(obj);
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
