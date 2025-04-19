using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData<TTarget,TEffect> : ScriptableObject where TEffect : IEffect<TTarget>
    {
        public float Duration;
        public StackingBehavior StackBehavior;
        public TickingBehavior TickBehavior;
        public int TicksPerSecond;

        public IEffectData<TEffect> Effect;
        private float TickInterval => 1f / TicksPerSecond;

        public IBuff GetBuff(TTarget target, GameObject source)
        {
            IEffect<TTarget> effect = Effect.GetEffect(source);
            IStackBehaviour stackBehavior = GetStackBehavior();
            ITickBehaviour tickBehavior = GetTickBehavior();
            return new Buff<TTarget>(effect, Duration, stackBehavior, tickBehavior,target);
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