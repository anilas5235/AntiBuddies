using System;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    public class CircleBatchSpawner : MonoBehaviour
    {
        [Header("Batch Settings")]
        [SerializeField] private GameObject prefabToSpawn;
        [SerializeField, Range(1,100)] private int spawnPerBatch = 5;
        [SerializeField, Range(.1f,20)] private float spawnDelay = 1f;
        [SerializeField, Range(1,15)] private int numberOfBatches = 2;
        [Header("Spawn Area")]
        [SerializeField, Range(1, 15)] private float spawnRadius = 3;

        private void Start()
        {
            StartCoroutine(SpawnBatches());
        }
        
        private IEnumerator SpawnBatches()
        {
            for (int batch = 0; batch < numberOfBatches; batch++)
            {
                SpawnBatch();
                
                if (batch < numberOfBatches - 1)
                {
                    yield return new WaitForSeconds(spawnDelay);
                }
            }
        }

        private void SpawnBatch()
        {
            if (!prefabToSpawn) throw new NullReferenceException("prefabToSpawn is null");
            
            for (int i = 0; i < spawnPerBatch; i++)
            {
                // Generate a random position within the circle collider
                Vector2 randomPos = UnityEngine.Random.insideUnitCircle * spawnRadius;
                Vector2 spawnPosition = (Vector2)transform.position + randomPos;

                // Instantiate the prefab at the calculated position
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Draw the spawn area
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
#endif
    }
}
