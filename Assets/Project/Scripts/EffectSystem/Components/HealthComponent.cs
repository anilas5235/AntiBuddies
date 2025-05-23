using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Package;
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
        public event Action OnDeath;
        public UnityEvent onDeath;

        private void OnEnable()
        {
            FullHeal();
            maxHpStat.Stat.OnStatChange += HandleMaxHealthChange;
        }
        
        private void OnDisable()
        {
            maxHpStat.Stat.OnStatChange -= HandleMaxHealthChange;
        }

        public int TakeDamage(int amount)
        {
            if (amount <= 0) return 0;
            CurrentHealth -= amount;
            return amount;
        }
        
        public int Heal(int amount)
        {
            if (amount <= 0 || CurrentHealth >= MaxHealth) return 0;
            int diff = MaxHealth - CurrentHealth;
            amount = diff < amount ? diff : amount;
            CurrentHealth += amount;
            return amount;
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
            maxHpStat.Init(statComponent);
        }
        
        private void HandleMaxHealthChange()
        {
            OnHealthChange?.Invoke();
        }
    }
}