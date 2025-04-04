using Project.Scripts.BuffSystem.Components;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class OnFire : TickingBuff<IDamageable>
    {
        public OnFire(float damage, float duration, float tickInterval, IDamageable target, GameObject source)
            : base(new FireAttack(source, damage), duration, StackBehavior.Stacking, target, tickInterval)
        {
        }
    }
}