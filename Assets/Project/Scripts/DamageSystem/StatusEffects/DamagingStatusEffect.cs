using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    public abstract class DamagingStatusEffect : BaseStatusEffect, IDamageDealer
    {
        protected IDamageable Target;
        protected IDamage DamagePerTick;
        
        public DamagingStatusEffect(IDamageable target, IDamage damagePerTick, float duration) : base(duration)
        {
            Target = target;
            DamagePerTick = damagePerTick;
        }

        public override void Tick()
        {
            Target.TakeDamage(GetAttack());
        }

        protected abstract AttackPackage GetAttack();
        
    }
}