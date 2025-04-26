using System;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.EffectSystem.Visuals;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private Stat health = new(0, 10, 0);
        public event Action<int, AttackType, GameObject> OnDamageReceived;
        public UnityEvent onDamageReceived;
        public event Action OnDeath;
        public UnityEvent onDeath;
        public event Action<int, HealType, GameObject> OnHealApplied;

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

        public void ApplyAttack(int amount, AttackType type)
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

        public void ApplyHeal(int amount, HealType type)
        {
            if (amount <= 0) return;
            health.IncreaseValue(amount);
            OnHealApplied?.Invoke(amount, type, gameObject);
        }
        
        public void ModifyHealth(int amount)
        {
            health.MaxValue += amount;
            if (health.MaxValue < health.MinValue)
            {
                Die();
                return;
            }
            if(amount > 0) health.IncreaseValue(amount);
        }

        public void FullHeal() => health.MaximizeValue();
    }
}