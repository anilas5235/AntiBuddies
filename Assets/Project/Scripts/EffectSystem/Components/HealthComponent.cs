using System;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private ClampedStat health = new(0, 10, 0);
        public event Action<EffectType, int> OnDamageReceived;

        public UnityEvent<EffectType, int> onDamageReceived;
        public event Action<EffectPackage> OnHealApplied;
        public event Action OnDeath;

        public UnityEvent onDeath;

        private void Awake()
        {
            FullHeal();
        }

        public void ApplyAttack(int amount, EffectType type)
        {
            if (amount <= 0) return;
            health.ReduceValue(amount);
            OnDamageReceived?.Invoke(type, amount);
            onDamageReceived?.Invoke(type, amount);
            if (IsDead()) Die();
        }

        public bool IsDead() => health.IsBelowOrZero();
        public bool IsAlive() => !IsDead();

        public void ApplyHeal(int amount, EffectType type)
        {
            if (amount <= 0) return;
            health.IncreaseValue(amount);
        }

        public void FullHeal() => health.MaximizeValue();
        public int MaxHealth => health.MaxValue;

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }
    }
}