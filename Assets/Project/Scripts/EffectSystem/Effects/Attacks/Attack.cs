using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public abstract class Attack: Effect<IDamageDealer, IDamageable>
    {
        protected float Amount;
        protected Attack(IDamageable target, IDamageDealer source, float amount, EffectType effectType) : base(target, source,effectType)
        {
            Amount = amount;
        }

        public override void Apply()
        {
            Target.TakeDamage(this);
        }
        
        public abstract int CalculateDamage();


        protected virtual int CalculateDamage(int flatDamageReduction, float resistance)
        {
            float damage = Amount;
            damage -= flatDamageReduction;
            damage *= 1 - resistance;
            return Mathf.RoundToInt(damage);
        }
    }
}