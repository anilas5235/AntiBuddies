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


        protected virtual int CalculateDamage(Stat flatDamageReduction, Stat resistance)
        {
            float damage = GetAmount();
            damage -= flatDamageReduction.Value;
            damage *= 1 - resistance.Percent;
            damage = Mathf.Max(damage, 0);
            return Mathf.RoundToInt(damage);
        }
    }
}