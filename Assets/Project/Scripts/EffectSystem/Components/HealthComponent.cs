using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using Project.Scripts.EffectSystem.Visuals;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private Stat health = new(0, 10,0);

        [SerializeField] private ResistanceComponent resistanceComponent;
        [SerializeField] private HealingStats healingStats;

        public event Action<GameObject,IAttack,int> OnDamageReceived;

        public UnityEvent onDamageReceived;
        public event Action OnDeath;

        public UnityEvent onDeath;

        private void Awake()
        {
            FullHeal();
        }

        private void OnEnable()
        {
            if (FloatingNumberSpawner.Instance) OnDamageReceived += FloatingNumberSpawner.Instance.SpawnDamageNumber;
        }
        
        private void OnDisable()
        {
            if (FloatingNumberSpawner.Instance) OnDamageReceived -= FloatingNumberSpawner.Instance.SpawnDamageNumber;
        }

        public bool IsDead() => health.IsBelowOrZero();

        public bool IsAlive() => !IsDead();

        public void FullHeal() => health.MaximizeValue();
        public int MaxHealth => health.MaxValue;

        public void Die()
        {
            Debug.Log($"<color=yellow>{gameObject.name} died </color>");
            OnDeath?.Invoke();
            onDeath?.Invoke();
        }

        public void Apply(IAttack attack)
        {
            int damage = attack.CalculateDamage(resistanceComponent);
            health.ReduceValue(damage);
            OnDamageReceived?.Invoke(gameObject,attack,damage);
            onDamageReceived?.Invoke();
            if (IsDead()) Die();
        }

        public void Apply(IHeal heal)
        {
            int amount = heal.CalculateHealing(healingStats,this);
            if (amount <= 0) return;
            health.IncreaseValue(amount);
        }
    }
}