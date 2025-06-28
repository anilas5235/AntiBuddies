using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Spawning.Components;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    public class SpawnerHandler : MonoBehaviour
    {
        private List<Spawner> spawnerScripts = new();
        public GameObject spawnerPrefab;
        public Vector2 groundSize;

        public void AddSpawners(List<Batch> batches)
        {
            if (batches == null || batches.Count == 0)
            {
                return;
            }
            foreach (Batch batch in batches)
            {
                float effectiveX = (groundSize.x / 2f) - batch.spawnRadius;
                float effectiveY = (groundSize.y / 2f) - batch.spawnRadius;
                Vector2 randomUnit = Random.insideUnitCircle;
                GameObject spawnerGO = Instantiate(spawnerPrefab,
                    new Vector2(randomUnit.x * effectiveX, randomUnit.y * effectiveY), Quaternion.identity, transform);
                Spawner spawner = spawnerGO.GetComponent<Spawner>();
                spawner.SetUp(batch);
                spawnerScripts.Add(spawner);
            }
        }

        public IEnumerator StartSpawners()
        {
            float maxDuration = 0f;
            foreach (Spawner spawner in spawnerScripts)
            {
                spawner.StartSpawning();
                float duration = spawner.SpawnDuration;
                if (duration > maxDuration)
                    maxDuration = duration;
            }

            yield return new WaitForSeconds(maxDuration);
            Destroy(gameObject);
        }
    }
}