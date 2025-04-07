using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(menuName = "BuffSystem/HealBuff", fileName = "HealBuff")]

    public class HealBuffData : BuffData<HealInfo, IHealable>
    {
        public override IBuff<IHealable> ToBuff(IHealable target, GameObject source)
        {
            return TickBehavior switch
            {
                TickBehavior.None => new Buff<IHealable>(Effect.ToEffect(source), Duration, StackBehavior, target),
                TickBehavior.Ticking => new TickingBuff<IHealable>(Effect.ToEffect(source), Duration, StackBehavior,
                    target, TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}