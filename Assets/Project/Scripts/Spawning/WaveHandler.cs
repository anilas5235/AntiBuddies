using System.Collections;
using Project.Scripts.ItemSystem;
using UnityEngine;

namespace Project.Scripts.Spawning
{
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

                // Show shop and pause game
                Time.timeScale = 0f;
                bool shopClosed = false;
                ShopUI.Instance.OnShopClosed += () => shopClosed = true;
                ShopUI.Instance.Show();
                // Wait until shop is closed
                yield return new WaitUntil(() => shopClosed);
                Time.timeScale = 1f;
            }
        }
    }
}