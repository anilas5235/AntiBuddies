using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumberSpawner : MonoBehaviour
    {
        public static FloatingNumberSpawner Instance;
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector2 offset = new(0, 0);
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

        internal void SpawnDamageNumber(GameObject eventSource, IAttack attack, int damage)
        {
            if (!damageNumberPrefab) return;

            FloatingNumber numberInstance =
                Instantiate(damageNumberPrefab,
                        eventSource.transform.position + (Vector3)offset, Quaternion.identity)
                    .GetComponent<FloatingNumber>();

            numberInstance.Setup(new FloatingNumberData(damage, attack.Color, displayDuration));
        }
    }
}