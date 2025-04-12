using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Project.Scripts.Spawning
{
    public class SpawnerHandler : MonoBehaviour
    {
        private List<CircleBatchSpawner> spawnerScripts = new List<CircleBatchSpawner>();
        public Vector2 groundSize;

        public void AddSpawners(List<Batch> batches)
        {
            foreach (var batch in batches)
            {
                GameObject spawnerGO = new GameObject("CircleBatchSpawner");
                spawnerGO.transform.parent = transform;
                float effectiveX = (groundSize.x / 2f) - batch.spawnRadius;
                float effectiveY = (groundSize.y / 2f) - batch.spawnRadius;
                Vector2 randomUnit = Random.insideUnitCircle;
                spawnerGO.transform.localPosition = new Vector2(randomUnit.x * effectiveX, randomUnit.y * effectiveY);
                CircleBatchSpawner spawner = spawnerGO.AddComponent<CircleBatchSpawner>();
                spawner.prefabToSpawn = batch.enemyPrefab;
                spawner.spawnPerBatch = batch.spawnPerBatch;
                spawner.spawnDelay = batch.spawnDelay;
                spawner.numberOfBatches = batch.numberOfBatches;
                spawner.spawnRadius = batch.spawnRadius;
                // Pass the initial delay from the batch to the spawner
                spawner.initialDelay = batch.initialDelay;
                spawnerScripts.Add(spawner);
            }
        }

        public IEnumerator StartSpawners()
        {
            float maxDuration = 0f;
            foreach (var spawner in spawnerScripts)
            {
                spawner.StartSpawning();
                float duration = spawner.initialDelay + spawner.spawnDelay * spawner.numberOfBatches;
                if (duration > maxDuration)
                    maxDuration = duration;
            }
            yield return new WaitForSeconds(maxDuration);
            Destroy(gameObject);
        }
    }
}
