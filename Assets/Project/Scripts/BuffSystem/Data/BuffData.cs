using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData<TTarget> : ScriptableObject
    {
        public float Duration;
        public StackBehavior StackBehavior;
        public TickBehavior TickBehavior;
        public int TicksPerSecond;

        public IEffect<TTarget> Effect;
        public float TickInterval => 1f / TicksPerSecond;

        public abstract IBuff<TTarget> ToBuff(TTarget target, GameObject source);
    }
}