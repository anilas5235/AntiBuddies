using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    public abstract class DamagingStatusEffect : BaseStatusEffect, IDamageDealer
    {
        private readonly IDamageable _target;
        protected readonly DamageInfo DamagePerTick;

        protected DamagingStatusEffect(IDamageable target, DamageInfo damagePerTick, float duration) : base(duration)
        {
            _target = target;
            DamagePerTick = damagePerTick;
        }

        public override void Tick(Component ticker)
        {
            DamageUtils.Attack(_target, DamagePerTick, ticker);
        }

        protected abstract DamageInfo GetAttack();
        
    }
}