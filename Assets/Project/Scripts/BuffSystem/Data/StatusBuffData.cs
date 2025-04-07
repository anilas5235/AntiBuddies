using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(menuName = "BuffSystem/StatusBuff", fileName = "StatusBuff")]

    public class StatusBuffData : BuffData<StatusInfo,IStatusEffectable>
    {
        public override IBuff<IStatusEffectable> ToBuff(IStatusEffectable target, GameObject source)
        {
            return TickBehavior switch
            {
                TickBehavior.None => new Buff<IStatusEffectable>(Effect.ToEffect(source), Duration, StackBehavior, target),
                TickBehavior.Ticking => new TickingBuff<IStatusEffectable>(Effect.ToEffect(source), Duration, StackBehavior,
                    target, TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}