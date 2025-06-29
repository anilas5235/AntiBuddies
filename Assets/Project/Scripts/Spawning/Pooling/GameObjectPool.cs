using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    /// <summary>
    /// Manages a pool of GameObjects for efficient reuse, minimizing instantiation and destruction.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
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

        /// <summary>
        /// Initializes the pool by filling it with the initial number of objects.
        /// </summary>
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

        /// <summary>
        /// Retrieves an object from the pool, activating it for use.
        /// </summary>
        /// <returns>An active IPoolable instance.</returns>
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

        /// <summary>
        /// Instantiates a new object and adds it to the pool.
        /// </summary>
        /// <returns>The newly created IPoolable instance.</returns>
        private IPoolable CreateNewInstance()
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            currentTotal++;
            IPoolable poolable = obj.GetComponent<IPoolable>();
            poolable.Init(this);
            return poolable;
        }

        /// <summary>
        /// Returns an object to the pool, deactivating and resetting it.
        /// </summary>
        /// <param name="obj">The IPoolable object to return.</param>
        public void AddToPool(IPoolable obj)
        {
            obj.Reset();
            obj.Deactivate();
            _pool.Push(obj);
            currentInPool = _pool.Count;
        }

        /// <summary>
        /// Indicates whether the pool is empty.
        /// </summary>
        private bool IsEmpty => _pool.Count == 0;

        /// <summary>
        /// Fills the pool up to the specified count.
        /// </summary>
        /// <param name="count">The desired number of objects in the pool.</param>
        private void FillPoolTo(int count)
        {
            while (_pool.Count < count)
            {
                AddToPool(CreateNewInstance());
            }
        }

        /// <summary>
        /// Sets a new prefab for the pool and reinitializes it.
        /// </summary>
        /// <param name="newPrefab">The new prefab to use.</param>
        internal void SetPrefab(GameObject newPrefab)
        {
            prefab = newPrefab;
            _initialized = false;
            Init();
        }

        private void OnDestroy()
        {
            _pool.Clear();
        }
    }
}