using System;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    /// <summary>
    /// Abstract ScriptableObject base for all buff data assets.
    /// Provides configuration for duration, stacking, and ticking behaviour.
    /// </summary>
    public abstract class BuffData : ScriptableObject
    {
        /// <summary>
        /// The duration of the buff in seconds.
        /// </summary>
        [SerializeField] protected float duration;

        /// <summary>
        /// The stacking behaviour type for this buff.
        /// </summary>
        [SerializeField] protected StackingBehavior stackBehavior;

        /// <summary>
        /// The ticking behaviour type for this buff.
        /// </summary>
        [SerializeField] protected TickingBehavior tickBehavior;

        /// <summary>
        /// Number of ticks per second for ticking buffs.
        /// </summary>
        [SerializeField] [Tooltip("only relevant if ticking")] private int ticksPerSecond;

        /// <summary>
        /// Gets the interval in seconds between each tick.
        /// </summary>
        private float TickInterval => 1f / ticksPerSecond;


        /// <summary>
        /// Gets the ITickBehaviour instance based on the selected tick behavior.
        /// </summary>
        /// <returns>The tick behaviour instance, or null if none.</returns>
        protected ITickBehaviour GetTickBehavior()
        {
            // Select tick behaviour implementation based on enum.
            return tickBehavior switch
            {
                TickingBehavior.None => null,
                TickingBehavior.Ticking => new Ticking(TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Gets the IStackBehaviour instance based on the selected stack behavior.
        /// </summary>
        /// <returns>The stack behaviour instance.</returns>
        protected IStackBehaviour GetStackBehavior()
        {
            // Select stack behaviour implementation based on enum.
            return stackBehavior switch
            {
                StackingBehavior.None => new NotStacking(),
                StackingBehavior.Refresh => new Refreshing(),
                StackingBehavior.Stacking => new Stacking(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Enum for ticking behaviour types.
        /// </summary>
        public enum TickingBehavior : byte
        {
            None,
            Ticking,
        }

        /// <summary>
        /// Enum for stacking behaviour types.
        /// </summary>
        protected enum StackingBehavior : byte
        {
            None,
            Refresh,
            Stacking,
        }
    }
}