using System;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Manages health, healing, and death logic for an entity.
    /// </summary>
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable, INeedStatGroup
    {
        /// <summary>
        /// The current health value.
        /// </summary>
        [SerializeField] private int currentHealth = 10;
        /// <summary>
        /// Reference to the max health stat.
        /// </summary>
        [SerializeField] private StatRef maxHpStat;
        /// <summary>
        /// The maximum health value.
        /// </summary>
        public int MaxHealth => maxHpStat.Stat.Value;

        /// <summary>
        /// The current health value (clamped between 0 and MaxHealth).
        /// </summary>
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

        /// <summary>
        /// The current health as a percentage of max health.
        /// </summary>
        public float HealthPercentage => (float)CurrentHealth / MaxHealth;
        /// <summary>
        /// Event triggered when health changes.
        /// </summary>
        public event Action OnHealthChange;
        /// <summary>
        /// Event triggered when the entity dies.
        /// </summary>
        public event Action OnDeath;
        /// <summary>
        /// Unity event triggered when the entity dies.
        /// </summary>
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

        /// <summary>
        /// Applies damage to the entity.
        /// </summary>
        /// <param name="amount">The amount of damage.</param>
        /// <returns>The actual damage applied.</returns>
        public int TakeDamage(int amount)
        {
            if (amount <= 0) return 0;
            CurrentHealth -= amount;
            return amount;
        }
        
        /// <summary>
        /// Heals the entity.
        /// </summary>
        /// <param name="amount">The amount to heal.</param>
        /// <returns>The actual amount healed.</returns>
        public int Heal(int amount)
        {
            if (amount <= 0 || CurrentHealth >= MaxHealth) return 0;
            int diff = MaxHealth - CurrentHealth;
            amount = diff < amount ? diff : amount;
            CurrentHealth += amount;
            return amount;
        }

        /// <summary>
        /// Checks if the entity is dead.
        /// </summary>
        /// <returns>True if dead, otherwise false.</returns>
        public bool IsDead() => CurrentHealth <= 0;
        /// <summary>
        /// Checks if the entity is alive.
        /// </summary>
        /// <returns>True if alive, otherwise false.</returns>
        public bool IsAlive() => !IsDead();

        /// <summary>
        /// Handles the entity's death.
        /// </summary>
        public void Die()
        {
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }

        /// <summary>
        /// Fully heals the entity to max health.
        /// </summary>
        public void FullHeal() => CurrentHealth = MaxHealth;

        /// <summary>
        /// Initializes the stat group for this component.
        /// </summary>
        /// <param name="statGroup">The stat group to use.</param>
        public void OnStatInit(IStatGroup statGroup)
        {
            maxHpStat.OnStatInit(statGroup);
        }
        
        /// <summary>
        /// Handles changes to the max health stat.
        /// </summary>
        private void HandleMaxHealthChange()
        {
            OnHealthChange?.Invoke();
        }
    }
}