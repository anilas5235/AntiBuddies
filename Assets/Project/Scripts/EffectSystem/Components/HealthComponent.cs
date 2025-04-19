using System;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Visuals;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private ClampedStat health = new(0, 10, 0);
        public int MaxHealth => health.MaxValue;
        public event Action<int, EffectType, GameObject> OnDamageReceived;
        public UnityEvent onDamageReceived;
        public event Action OnDeath;
        public UnityEvent onDeath;
        public event Action<int, EffectType, GameObject> OnHealApplied;

        private void Awake()
        {
            FullHeal();
        }

        private void OnEnable()
        {
            OnDamageReceived += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }

        private void OnDisable()
        {
            OnDamageReceived -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }

        public void ApplyAttack(int amount, EffectType type)
        {
            if (amount <= 0) return;
            health.ReduceValue(amount);
            OnDamageReceived?.Invoke(amount, type, gameObject);
            onDamageReceived?.Invoke();
            if (IsDead()) Die();
        }

        public bool IsDead() => health.IsBelowOrZero();

        public bool IsAlive() => !IsDead();

        public void Die()
        {
            Debug.Log($"{gameObject.name} died");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }

        public void ApplyHeal(int amount, EffectType type)
        {
            if (amount <= 0) return;
            health.IncreaseValue(amount);
            OnHealApplied?.Invoke(amount, type, gameObject);
        }

        public void FullHeal() => health.MaximizeValue();
    }
}