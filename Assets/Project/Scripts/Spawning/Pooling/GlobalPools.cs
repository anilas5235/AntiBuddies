using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    /// <summary>
    /// Singleton managing all GameObject pools globally, allowing retrieval and creation of pools for prefabs.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [DefaultExecutionOrder(-150)]
    public class GlobalPools : Singleton<GlobalPools>
    {
        private readonly Dictionary<GameObject, GameObjectPool> _pools = new();

        /// <summary>
        /// Gets the pool for a specific prefab type, creating one if it doesn't exist.
        /// </summary>
        /// <param name="type">The prefab to get a pool for.</param>
        /// <returns>The GameObjectPool for the prefab.</returns>
        public GameObjectPool GetPoolFor(GameObject type)
        {
            if (!type)
            {
                Debug.LogWarning("Requested pool for null type.");
                return null;
            }

            return _pools.TryGetValue(type, out GameObjectPool pool) ? pool : CreateNewPool(type);
        }

        /// <summary>
        /// Creates a new pool for the given prefab and registers it.
        /// </summary>
        /// <param name="prefab">The prefab to create a pool for.</param>
        /// <returns>The created GameObjectPool.</returns>
        private GameObjectPool CreateNewPool(GameObject prefab)
        {
            if (!prefab)
            {
                return null;
            }

            GameObjectPool newPool = gameObject.AddComponent<GameObjectPool>();
            newPool.SetPrefab(prefab);
            _pools.Add(prefab, newPool);
            return newPool;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _pools.Clear();
        }
    }
}