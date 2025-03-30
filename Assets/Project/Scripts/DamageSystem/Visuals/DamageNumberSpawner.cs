using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using Project.Scripts.DamageSystem.Events;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    public class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private float displayDuration = 1.0f;
        [SerializeField] private Vector2 offset = new(0, 0);
        [SerializeField] private float floatSpeed = 1.0f;

        private HealthComponent _healthComponent;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            _healthComponent.OnDamageReceived += HandleDamageReceived;
        }

        private void OnDisable()
        {
            _healthComponent.OnDamageReceived -= HandleDamageReceived;
        }

        private void HandleDamageReceived(DamageEvent damageEvent)
        {
            if (!damageNumberPrefab) return;

            SpawnDamageNumber(damageEvent.Damage);
        }

        private void SpawnDamageNumber(DamageInfo damage)
        {
            FloatingDamageNumber damageNumberInstance = Instantiate(damageNumberPrefab, transform.position + (Vector3)offset, Quaternion.identity)
                .GetComponent<FloatingDamageNumber>();


            damageNumberInstance.Setup(damage, displayDuration);
            damageNumberInstance.transform.SetParent(transform);
        }
    }
}