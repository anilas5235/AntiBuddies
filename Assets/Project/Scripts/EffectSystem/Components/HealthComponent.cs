using System;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth = 10;

        [SerializeField] private ResistanceComponent resistanceComponent;

        public event Action<AttackInfo> OnDamageReceived;

        public UnityEvent onDamageReceived;
        public event Action OnDeath;

        public UnityEvent onDeath;

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

        public void TakeDamage(Attack attack)
        {
            int damage = attack.CalculateDamage(resistanceComponent);
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke(new AttackInfo(damage,attack.AttackType));
            onDamageReceived?.Invoke();
        }

        public bool IsDead() => currentHealth <= 0;

        public bool IsAlive() => !IsDead();

        public ResistanceComponent GetResistance() => resistanceComponent;

        public void Heal(int amount) => currentHealth += amount;

        public void FullHeal() => currentHealth = maxHealth;

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }
    }
}