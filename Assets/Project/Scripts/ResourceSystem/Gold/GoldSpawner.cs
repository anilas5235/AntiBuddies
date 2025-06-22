using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.ResourceSystem.Gold
{
    /// <summary>
    /// Spawns gold objects at a given position, using pooling and random placement within a radius.
    /// </summary>
    public class GoldSpawner : Singleton<GoldSpawner>
    {
        /// <summary>
        /// Prefab for the gold object.
        /// </summary>
        [SerializeField] private GameObject goldPrefab;

        /// <summary>
        /// Radius within which gold is spawned around the given position.
        /// </summary>
        [SerializeField] private float radius = 1.0f;

        /// <summary>
        /// If true, prevents gold from being spawned.
        /// </summary>
        [SerializeField] private bool stopSpawn;

        /// <summary>
        /// Pool for gold objects.
        /// </summary>
        private GameObjectPool _goldPool;

        private void OnEnable()
        {
            _goldPool = GlobalPools.Instance.GetPoolFor(goldPrefab);
        }

        /// <summary>
        /// Spawns a specified amount of gold at the given position.
        /// </summary>
        /// <param name="amount">Number of gold objects to spawn.</param>
        /// <param name="position">World position to spawn gold around.</param>
        public void SpawnGold(int amount, Vector3 position)
        {
            if (stopSpawn) return;
            if (!_goldPool)
            {
                Debug.LogWarning("FloatingNumberPool is null");
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                Gold goldInstance = (Gold)_goldPool.GetObject();
                if (!goldInstance)
                {
                    Debug.LogWarning("GoldPool returned null");
                    return;
                }

                // Place gold randomly within a circle of the given radius.
                goldInstance.transform.position = position + (Vector3)Random.insideUnitCircle * radius;
            }
        }
    }
}