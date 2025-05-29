using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.ResourceSystem
{
    public class GoldSpawner : Singleton<GoldSpawner>
    {
        [SerializeField] private GameObject goldPrefab;
        [SerializeField] private float radius = 1.0f;
        [SerializeField] private bool stopSpawn;
        private GameObjectPool _goldPool;

        private void OnEnable()
        {
            _goldPool = GlobalPools.Instance.GetPoolFor(goldPrefab);
        }

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
                    Debug.LogWarning("FloatingNumberPool returned null");
                    return;
                }

                goldInstance.transform.position = position + Random.insideUnitSphere * radius;
            }
        }
    }
}