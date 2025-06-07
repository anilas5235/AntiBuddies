using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int initialObjCount = 10;
        [SerializeField] private int currentTotal;
        [SerializeField] private int currentInPool;
        private readonly Stack<IPoolable> _pool = new();
        private bool _initialized;

        private void OnEnable()
        {
            if (!_initialized && prefab)
            {
                Init();
            }
        }

        private void Init()
        {
            if (_initialized)
            {
                return;
            }

            if (!prefab)
            {
                Debug.LogError("Prefab is not assigned in GameObjectPool. Cannot initialize pool.");
                return;
            }

            FillPoolTo(initialObjCount);
            _initialized = true;
        }

        public IPoolable GetObject()
        {
            if (!_initialized)
            {
                Debug.LogError("GameObjectPool not initialized. Call Init() before using GetObject().");
                return null;
            }

            IPoolable obj = IsEmpty ? CreateNewInstance() : _pool.Pop();
            currentInPool = _pool.Count;
            obj.Activate();
            return obj;
        }

        private IPoolable CreateNewInstance()
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            currentTotal++;
            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable.Init(this);
            return poolable;
        }

        public void AddToPool(IPoolable obj)
        {
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

        internal void SetPrefab(GameObject newPrefab)
        {
            prefab = newPrefab;
            _initialized = false;
            Init();
        }
    }
}