using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.ResourceSystem
{
    public class GoldSpawner : Singleton<GoldSpawner>
    {
        [SerializeField] private GameObjectPool goldPool;
        [SerializeField] private float radius = 1.0f;
        [SerializeField] private bool stopSpawn;

        public void SpawnGold(int amount, Vector3 position)
        {
            if (stopSpawn) return;
            if (!goldPool)
            {
                Debug.LogWarning("FloatingNumberPool is null");
                return;
            }

            for (int i = 0; i < amount; i++)
            {
                Gold goldInstance = (Gold)goldPool.GetObject();
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