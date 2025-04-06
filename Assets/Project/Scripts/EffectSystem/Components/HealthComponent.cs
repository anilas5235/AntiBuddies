using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Positive;
using Project.Scripts.EffectSystem.Resistance;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth = 10;

        [SerializeField] protected ResistanceData resistances;

        public event Action<AttackInfo> OnDamageReceived;

        public UnityEvent OnDamageReceivedUE;
        public event Action OnDeath;

        public UnityEvent OnDeathUE;

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
            int damage = attack.CalculateDamage(resistances);
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke(new AttackInfo(damage,attack.AttackType));
            OnDamageReceivedUE?.Invoke();
        }

        public bool IsDead() => currentHealth <= 0;

        public bool IsAlive() => !IsDead();

        public ResistanceData GetResistanceData() => resistances;

        public void Heal(int amount) => currentHealth += amount;

        public void FullHeal() => currentHealth = maxHealth;

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            OnDeathUE?.Invoke();
        }
    }
}