using System.Collections.Generic;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    [DefaultExecutionOrder(-150)]
    public class GlobalPools : Singleton<GlobalPools>
    {
        private readonly Dictionary<GameObject, GameObjectPool> _pools = new();


        public GameObjectPool GetPoolFor(GameObject type)
        {
            if (!type)
            {
                Debug.LogWarning("Requested pool for null type.");
                return null;
            }

            return _pools.TryGetValue(type, out GameObjectPool pool) ? pool : CreateNewPool(type);
        }

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
    }
}