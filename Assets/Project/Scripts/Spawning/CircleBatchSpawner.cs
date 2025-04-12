using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawning
{
    public class CircleBatchSpawner : MonoBehaviour
    {
        [Header("Batch Settings")] [SerializeField]
        public GameObject prefabToSpawn;

        [SerializeField, Range(1, 100)] public int spawnPerBatch = 5;
        [SerializeField, Range(.1f, 20)] public float spawnDelay = 1f;
        [SerializeField, Range(1, 15)] public int numberOfBatches = 2;
        [SerializeField, Range(0f, 20f)] public float initialDelay = 0f;

        [Header("Spawn Area")] [SerializeField, Range(1, 15)]
        public float spawnRadius = 3;

        private IEnumerator SpawnBatches()
        {
            yield return new WaitForSeconds(initialDelay);
            WaitForSeconds waitDelay = new WaitForSeconds(spawnDelay); // Cached WaitForSeconds
            for (int batch = 0; batch < numberOfBatches; batch++)
            {
                SpawnBatch();
                if (batch < numberOfBatches - 1)
                {
                    yield return waitDelay;
                }
            }
            Destroy(gameObject);
        }

        private void SpawnBatch()
        {
            if (!prefabToSpawn) throw new NullReferenceException("prefabToSpawn is null");
            Vector2 basePos = transform.position; // Cache position
            for (int i = 0; i < spawnPerBatch; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
                Vector2 spawnPosition = basePos + randomPos;
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnBatches());
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
#endif
    }
}

