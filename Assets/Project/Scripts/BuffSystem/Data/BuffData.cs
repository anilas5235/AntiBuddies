using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData<TTarget> : ScriptableObject
    {
        public float Duration;
        public StackingBehavior StackBehavior;
        public TickingBehavior TickBehavior;
        public int TicksPerSecond;

        public IEffect<TTarget> Effect;
        public float TickInterval => 1f / TicksPerSecond;

        public abstract IBuff ToBuff(TTarget target, GameObject source);
        
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