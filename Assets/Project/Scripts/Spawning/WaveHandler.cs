using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Spawning
{
    public class WaveGroup : ScriptableObject
    {
        public List<Wave> waves = new();
    }

    public class WaveHandler : MonoBehaviour
    {
        [SerializeField] WaveGroup waveGroup;
        public GameObject spawnerHandlerPrefab;

        void Start()
        {
            StartCoroutine(ManageWaves());
        }

        private IEnumerator ManageWaves()
        {
            Vector2 computedGroundSize = new Vector2(transform.localScale.x, transform.localScale.y);
            foreach (var wave in waveGroup.waves)
            {
                GameObject handlerGO = Instantiate(spawnerHandlerPrefab);
                SpawnerHandler spawnerHandler = handlerGO.GetComponent<SpawnerHandler>();
                spawnerHandler.groundSize = computedGroundSize;
                spawnerHandler.AddSpawners(wave.batches);
                yield return StartCoroutine(spawnerHandler.StartSpawners());
                WaitForSeconds waitWave = new WaitForSeconds(wave.timeBetweenWaves);
                yield return waitWave;
            }
        }
    }
}