using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.EffectSystem.Visuals;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable, INeedStatComponent
    {
        [SerializeField] private int currentHealth = 10;
        [SerializeField] private StatRef maxHpStat;

        private StatComponent _statComponent;
        private int MaxHealth => maxHpStat.Stat.Value;

        private int CurrentHealth
        {
            get => currentHealth;
            set
            {
                if (currentHealth == value) return;
                currentHealth = Math.Clamp(value, 0, MaxHealth);
                if (IsDead()) Die();
            }
        }

        public event Action<int, AttackType, GameObject> OnDamageReceived;
        public UnityEvent onDamageReceived;
        public event Action OnDeath;
        public UnityEvent onDeath;
        public event Action<int, HealType, GameObject> OnHealApplied;

        private void OnEnable()
        {
            FullHeal();
            OnDamageReceived += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }

        private void OnDisable()
        {
            OnDamageReceived -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }

        public void ApplyAttack(EffectPackage<AttackType> package)
        {
            int damage = package.Amount;
            AttackType attackType = package.EffectType;

            if (attackType.AffectedByFlatModifier)
            {
                Stat flatModifier = _statComponent.GetStat(attackType.FlatModifier);
                if (flatModifier != null) damage = flatModifier.TransformNegative(damage);
            }

            if (attackType.AffectedByPercentModifier)
            {
                Stat percentageModifier = _statComponent.GetStat(attackType.PercentModifier);
                if (percentageModifier != null) damage = percentageModifier.TransformNegative(damage);
            }

            if (damage <= 0) return;
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke(damage, attackType, gameObject);
            onDamageReceived?.Invoke();
        }

        public bool IsDead() => CurrentHealth <= 0;
        public bool IsAlive() => !IsDead();

        public void Die()
        {
            Debug.Log($"{gameObject.name} died");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }

        public void ApplyHeal(EffectPackage<HealType> package)
        {
            int amount = package.Amount;
            if (amount <= 0 || CurrentHealth >= MaxHealth) return;
            int diff = MaxHealth - CurrentHealth;
            CurrentHealth += amount;
            OnHealApplied?.Invoke(diff < amount ? diff : amount, package.EffectType, gameObject);
        }

        public void FullHeal() => CurrentHealth = MaxHealth;

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
            maxHpStat.GetStat(statComponent);
        }
    }
}