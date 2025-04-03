using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public abstract class Attack: Effect<IDamageDealer, IDamageable>
    {
        protected float Amount;
        protected Attack(IDamageDealer source, float amount, EffectType effectType) : base(source,effectType)
        {
            Amount = amount;
        }

        public override void Apply(IDamageable target)
        {
            target.TakeDamage(this);
        }
        public abstract int CalculateDamage(ResistanceData resData);


        protected virtual int CalculateDamage(int flatDamageReduction, float resistance)
        {
            float damage = Amount;
            damage -= flatDamageReduction;
            damage *= 1 - resistance;
            return Mathf.RoundToInt(damage);
        }
    }
}