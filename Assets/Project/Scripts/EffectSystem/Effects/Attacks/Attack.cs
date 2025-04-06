using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public abstract class Attack : Effect<IDamageable>
    {
        private readonly AttackType _attackType;
        public AttackType AttackType => _attackType;

        protected Attack(GameObject source, int amount, AttackType attackType) : base(source,amount)
        {
            _attackType = attackType;
        }

        public override void Apply(IDamageable target)
        {
            target.TakeDamage(this);
        }

        public abstract int CalculateDamage(ResistanceComponent resistanceComponent);


        protected virtual int CalculateDamage(Stat flatDamageReduction, PercentStat resistance)
        {
            int damage = GetAmount();
            // Apply flat damage reduction
            damage = flatDamageReduction.GetTransformedValue(damage);
            if(damage <= 0) return 0;
            
            // Apply resistance
            damage = resistance.GetTransformedValue(damage);
            return damage <= 0 ? 0 : Mathf.RoundToInt(damage);
        }
    }
}