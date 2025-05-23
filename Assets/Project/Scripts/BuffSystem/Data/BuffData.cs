using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData : ScriptableObject
    {
        [SerializeField] protected float duration;
        [SerializeField] protected StackingBehavior stackBehavior;
        [SerializeField] protected TickingBehavior tickBehavior;
        [SerializeField] private int ticksPerSecond;

        private float TickInterval => 1f / ticksPerSecond;

        #region Behaviours

        protected ITickBehaviour GetTickBehavior()
        {
            return tickBehavior switch
            {
                TickingBehavior.None => null,
                TickingBehavior.Ticking => new Ticking(TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected IStackBehaviour GetStackBehavior()
        {
            return stackBehavior switch
            {
                StackingBehavior.None => new NotStacking(),
                StackingBehavior.Refresh => new Refreshing(),
                StackingBehavior.Stacking => new Stacking(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected enum TickingBehavior : byte
        {
            None,
            Ticking,
        }

        protected enum StackingBehavior : byte
        {
            None,
            Refresh,
            Stacking,
        }

        #endregion
    }
}