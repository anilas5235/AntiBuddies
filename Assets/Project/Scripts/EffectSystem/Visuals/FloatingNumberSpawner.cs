using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    public class FloatingNumberSpawner : MonoBehaviour
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

        private void HandleDamageReceived(IAttack attack, int damage)
        {
            if (!damageNumberPrefab) return;

            FloatingNumber numberInstance =
                Instantiate(damageNumberPrefab, transform.position + (Vector3)offset, Quaternion.identity)
                    .GetComponent<FloatingNumber>();

            numberInstance.Setup(new FloatingNumberData(damage, attack.Color, displayDuration));
        }
    }
}