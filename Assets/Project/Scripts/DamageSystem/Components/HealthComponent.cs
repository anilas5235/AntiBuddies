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

        public int MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
            }
        }

        private void Awake()
        {
            FullHeal();
        }
        
        public void TakeDamage(DamageInfo damageInfo, Component attacker)
        {
            if (damageInfo == null || damageInfo.GetDamage() <= 0) return;
            CurrentHealth -= DamageUtils.CalcDamage(damageInfo,resistances);
            OnDamageReceived?.Invoke(new DamageEvent(damageInfo, attacker, gameObject));
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
        }
        
        public void FullHeal()
        {
            currentHealth = maxHealth;
        }

        protected void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
        }
    }
}