using System;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth = 10;

        public Action OnDeath;

        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;

        private void Awake()
        {
            FullHeal();
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0) return;
            currentHealth -= damage;
            if (currentHealth <= 0) Die();
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
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