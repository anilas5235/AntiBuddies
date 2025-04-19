using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawning
{
    public class CircleBatchSpawner : MonoBehaviour
    {
        [Header("Batch Settings")] [SerializeField]
        public Batch batch;
        private IEnumerator SpawnBatches()
        {
            yield return new WaitForSeconds(batch.initialDelay);
            WaitForSeconds waitDelay = new WaitForSeconds(batch.spawnDelay); // Cached WaitForSeconds
            for (int i = 0; i < batch.numberOfBatches; i++)
            {
                SpawnBatch();
                if (i < batch.numberOfBatches - 1)
                {
                    yield return waitDelay;
                }
            }
            Destroy(gameObject);
        }

        private void SpawnBatch()
        {
            if (!batch.enemyPrefab) throw new NullReferenceException("prefabToSpawn is null");
            Vector2 basePos = transform.position; // Cache position
            for (int i = 0; i < batch.spawnPerBatch; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle * batch.spawnRadius;
                Vector2 spawnPosition = basePos + randomPos;
                Instantiate(batch.enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnBatches());
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!batch) return;
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, batch.spawnRadius);
        }
#endif
    }
}

