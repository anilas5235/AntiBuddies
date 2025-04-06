using System;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private Stat health = new(0, 10);

        [SerializeField] private ResistanceComponent resistanceComponent;

        public event Action<AttackInfo> OnDamageReceived;

        public UnityEvent onDamageReceived;
        public event Action OnDeath;

        public UnityEvent onDeath;

        private void Awake()
        {
            FullHeal();
        }

        public void TakeDamage(Attack attack)
        {
            int damage = attack.CalculateDamage(resistanceComponent);
            health.Value -= damage;
            OnDamageReceived?.Invoke(new AttackInfo(damage, attack.AttackType));
            onDamageReceived?.Invoke();
            if (health.Value <= 0) Die();
        }

        public bool IsDead() => health.Value <= 0;

        public bool IsAlive() => !IsDead();

        public ResistanceComponent GetResistance() => resistanceComponent;

        public void Heal(int amount) => health.Value += amount;

        public void FullHeal() => health.Value = health.MaxValue;

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }
    }
}