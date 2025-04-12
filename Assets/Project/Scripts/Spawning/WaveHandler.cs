using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    public class WaveHandler : MonoBehaviour
    {
        [SerializeField] public List<Wave> waves;

        void Start()
        {
            StartCoroutine(ManageWaves());
        }

        private IEnumerator ManageWaves()
        {
            Vector2 computedGroundSize = new Vector2(transform.localScale.x, transform.localScale.y);
            foreach (var wave in waves)
            {
                GameObject handlerGO = new GameObject("SpawnerHandler");
                SpawnerHandler spawnerHandler = handlerGO.AddComponent<SpawnerHandler>();
                spawnerHandler.groundSize = computedGroundSize;
                spawnerHandler.AddSpawners(wave.batches);
                yield return StartCoroutine(spawnerHandler.StartSpawners());
                WaitForSeconds waitWave = new WaitForSeconds(wave.timeBetweenWaves);
                yield return waitWave;
            }
        }
    }
}
