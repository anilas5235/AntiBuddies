using System;
using Project.Scripts.EffectSystem.Effects.Data;
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
        public int MaxHealth => maxHpStat.Stat.Value;

        public int CurrentHealth
        {
            get => currentHealth;
            private set
            {
                value = Mathf.Clamp(value, 0, MaxHealth);
                if (currentHealth == value) return;
                currentHealth = value;
                OnHealthChange?.Invoke();
                if (IsDead()) Die();
            }
        }

        public float HealthPercentage => (float)CurrentHealth / MaxHealth;
        public event Action OnHealthChange;

        public event Action<int, DamageType, GameObject> OnDamageReceived;
        public UnityEvent onDamageReceived;
        public event Action OnDeath;
        public UnityEvent onDeath;
        public event Action<int, HealType, GameObject> OnHealApplied;

        private void OnEnable()
        {
            FullHeal();
            OnDamageReceived += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            maxHpStat.Stat.OnStatChange += HandleMaxHealthChange;
        }
        
        private void OnDisable()
        {
            OnDamageReceived -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealApplied -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            maxHpStat.Stat.OnStatChange -= HandleMaxHealthChange;
        }

        public bool IsDead() => CurrentHealth <= 0;
        public bool IsAlive() => !IsDead();

        public void Die()
        {
            Debug.Log($"{gameObject.name} died");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }

        public void FullHeal() => CurrentHealth = MaxHealth;

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
            maxHpStat.Init(statComponent);
        }

        public void Apply(EffectPackage<DamageType> package)
        {
            int damage = package.Amount;
            if (_statComponent) damage = package.EffectType.ReceptionScale(damage, _statComponent, null);
            if (damage <= 0) return;
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke(damage, package.EffectType, gameObject);
            onDamageReceived?.Invoke();
        }

        public void Apply(EffectPackage<HealType> package)
        {
            int amount = package.Amount;
            if (_statComponent) amount = package.EffectType.ReceptionScale(amount, _statComponent, null);
            if (amount <= 0 || CurrentHealth >= MaxHealth) return;
            int diff = MaxHealth - CurrentHealth;
            CurrentHealth += amount;
            OnHealApplied?.Invoke(diff < amount ? diff : amount, package.EffectType, gameObject);
        }
        
        private void HandleMaxHealthChange()
        {
            OnHealthChange?.Invoke();
        }
    }
}