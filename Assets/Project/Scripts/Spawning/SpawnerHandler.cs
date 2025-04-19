using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    public class SpawnerHandler : MonoBehaviour
    {
        private List<CircleBatchSpawner> spawnerScripts = new();
        public GameObject spawnerPrefab;
        public Vector2 groundSize;

        public void AddSpawners(List<Batch> batches)
        {
            foreach (var batch in batches)
            {
                float effectiveX = (groundSize.x / 2f) - batch.spawnRadius;
                float effectiveY = (groundSize.y / 2f) - batch.spawnRadius;
                Vector2 randomUnit = Random.insideUnitCircle;
                GameObject spawnerGO = Instantiate(spawnerPrefab,
                    new Vector2(randomUnit.x * effectiveX, randomUnit.y * effectiveY), Quaternion.identity, transform);
                CircleBatchSpawner spawner = spawnerGO.GetComponent<CircleBatchSpawner>();
                spawner.batch = batch;
                spawnerScripts.Add(spawner);
            }
        }

        public IEnumerator StartSpawners()
        {
            float maxDuration = 0f;
            foreach (var spawner in spawnerScripts)
            {
                spawner.StartSpawning();
                float duration = spawner.batch.initialDelay + spawner.batch.spawnDelay * spawner.batch.numberOfBatches;
                if (duration > maxDuration)
                    maxDuration = duration;
            }

            yield return new WaitForSeconds(maxDuration);
            Destroy(gameObject);
        }
    }
}