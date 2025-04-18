using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewBuffData", menuName = "BuffSystem/BuffData")]
    public class BuffData : ScriptableObject
    {
        public float Duration;
        public StackingBehavior StackBehavior;
        public TickingBehavior TickBehavior;
        public int TicksPerSecond;

        public EffectData Effect;
        private float TickInterval => 1f / TicksPerSecond;

        public IBuff GetBuff(ITarget<EffectPackage> target, GameObject source, AlieGroup alieGroup)
        {
            EffectPackage effect = Effect.GetPackage(source,alieGroup);
            IStackBehaviour stackBehavior = GetStackBehavior();
            ITickBehaviour tickBehavior = GetTickBehavior();
            return new Buff(effect, Duration, stackBehavior, tickBehavior,target);
        }

        private ITickBehaviour GetTickBehavior()
        {
            return TickBehavior switch
            {
                TickingBehavior.None => new NonTicking(),
                TickingBehavior.Ticking => new Ticking(TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IStackBehaviour GetStackBehavior()
        {
            return StackBehavior switch
            {
                StackingBehavior.None => new NotStacking(),
                StackingBehavior.Refresh => new Refreshing(),
                StackingBehavior.Stacking => new Stacking(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public enum TickingBehavior : byte
        {
            None,
            Ticking,
        }
        
        public enum StackingBehavior : byte
        {
            None,
            Refresh,
            Stacking,
        }
    }
}