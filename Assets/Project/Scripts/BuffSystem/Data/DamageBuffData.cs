using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(menuName = "BuffSystem/DamageBuff", fileName = "DamageBuff")]
    public class DamageBuffData : BuffData<AttackInfo, IDamageable>
    {
        public override IBuff<IDamageable> ToBuff(IDamageable target, GameObject source)
        {
            return TickBehavior switch
            {
                TickBehavior.None => new Buff<IDamageable>(Effect.ToEffect(source), Duration, StackBehavior, target),
                TickBehavior.Ticking => new TickingBuff<IDamageable>(Effect.ToEffect(source), Duration, StackBehavior,
                    target, TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}