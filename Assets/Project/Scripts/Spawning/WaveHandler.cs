using System.Collections;
using Project.Scripts.ItemSystem;
using Project.Scripts.UI;
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
            Vector2 computedGroundSize = new(transform.localScale.x, transform.localScale.y);
            for (int i = 0; i < waveGroup.waves.Count; i++)
            {
                Wave wave = waveGroup.waves[i];
                GameObject handlerGO = Instantiate(spawnerHandlerPrefab);
                SpawnerHandler spawnerHandler = handlerGO.GetComponent<SpawnerHandler>();
                spawnerHandler.groundSize = computedGroundSize;
                spawnerHandler.AddSpawners(wave.batches);
                yield return StartCoroutine(spawnerHandler.StartSpawners());
                WaitForSeconds waitWave = new(wave.timeBetweenWaves);
                yield return waitWave;

                // Show shop and pause game
                if (i + 1 >= waveGroup.waves.Count) continue;
                
                UIManager.Instance.ToggleShop();
                yield return new WaitUntil(() => !ShopUI.Instance.IsShopOpen);
            }

            UIManager.Instance.ShowEndRunMenu(true);
        }
    }
}