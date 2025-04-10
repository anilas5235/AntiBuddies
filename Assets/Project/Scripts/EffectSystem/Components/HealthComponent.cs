using System;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private Stat health = new(0, 10,0);

        [SerializeField] private ResistanceComponent resistanceComponent;
        [SerializeField] private HealingStats healingStats;

        public event Action<AttackInfo> OnDamageReceived;

        public UnityEvent onDamageReceived;
        public event Action OnDeath;

        public UnityEvent onDeath;

        private void Awake()
        {
            FullHeal();
        }

        public int ApplyDamage(IAttack attack)
        {
            int damage = attack.CalculateDamage(resistanceComponent);
            health.ReduceValue(damage);
            OnDamageReceived?.Invoke(new AttackInfo(damage, attack.AttackType));
            onDamageReceived?.Invoke();
            if (IsDead()) Die();
            return damage;
        }

        public bool IsDead() => health.IsBelowOrZero();

        public bool IsAlive() => !IsDead();
        public void Heal(IHeal amount)
        {
            int heal = amount.CalculateHealing(healingStats);
            if (heal <= 0) return;
            health.IncreaseValue(heal);
        }

        public void FullHeal() => health.MaximizeValue();

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }
    }
}