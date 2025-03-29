using System;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Events;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth = 10;
        
        [SerializeField] protected ResistanceData resistances;

        public event Action<DamageEvent> OnDamageReceived;
        public event Action OnDeath;

        public int CurrentHealth
        {
            get => currentHealth;
            protected set
            {
                currentHealth = Math.Clamp(value, 0, maxHealth);
                if (currentHealth <= 0) Die();
            }
        }

        public int MaxHealth => maxHealth;

        private void Awake()
        {
            FullHeal();
        }
        
        public void TakeDamage(DamageInfo attackPackage, Component attacker)
        {
            if (attackPackage == null || attackPackage.GetDamage() <= 0) return;
            CurrentHealth -= DamageUtils.CalcDamage(attackPackage,resistances);
            OnDamageReceived?.Invoke(new DamageEvent(CurrentHealth, attackPackage.damageType, attacker, gameObject));
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
        }
        
        public void SetMaxHealth(int amount)
        {
            maxHealth = amount;
            currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        }

        public void FullHeal()
        {
            currentHealth = maxHealth;
        }

        protected void Die()
        {
            OnDeath?.Invoke();
        }
    }
}