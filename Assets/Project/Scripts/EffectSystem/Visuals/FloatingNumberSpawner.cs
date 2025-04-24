using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumberSpawner : MonoBehaviour
    {
        public static FloatingNumberSpawner Instance;
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector2 offset = new(0, 0.5f);
        [SerializeField] private bool stopSpawn;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SpawnFloatingNumber(int num,EffectType effectType, GameObject source)
        {
            if (!damageNumberPrefab) return;

            FloatingNumber numberInstance =
                Instantiate(damageNumberPrefab, source.transform.position + (Vector3)offset, Quaternion.identity)
                    .GetComponent<FloatingNumber>();

            numberInstance.Setup(new FloatingNumberData(num, effectType.Color, displayDuration));
        }
    }
}