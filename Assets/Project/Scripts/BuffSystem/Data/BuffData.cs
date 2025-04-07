using System;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData<TEffectInfo,TTarget> : ScriptableObject
    {
        public float Duration;
        public StackBehavior StackBehavior;
        public TickBehavior TickBehavior;
        public int TicksPerSecond;

        public TEffectInfo Effect;
        public float TickInterval => 1f / TicksPerSecond;

        public abstract IBuff<TTarget> ToBuff(TTarget target, GameObject source);
    }
}