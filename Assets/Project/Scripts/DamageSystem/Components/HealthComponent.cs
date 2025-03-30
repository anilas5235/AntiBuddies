using System;
using System.Collections.Generic;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Events;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class HealthComponent : MonoBehaviour, IEffectable
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth = 10;
        
        [SerializeField] protected ResistanceData resistances;

        public event Action<EffectEvent> OnDamageReceived;
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
        
        public void Apply(EffectInfo effectInfo, Component source)
        {
            if (effectInfo == null || effectInfo.GetAmount() <= 0) return;
            CurrentHealth -= DamageUtils.CalcDamage(effectInfo,resistances);
            OnDamageReceived?.Invoke(new EffectEvent(effectInfo, source, gameObject));
        }

        public void Apply(List<EffectInfo> effectInfos, Component source)
        {
            foreach (EffectInfo effectInfo in effectInfos)
            {
                Apply(effectInfo, source);
            }
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