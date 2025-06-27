using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// Represents a group of buffs that are ticked together and have a maximum capacity.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public class BuffGroup
    {
        /// <summary>
        /// The maximum number of buffs allowed in this group.
        /// </summary>
        private const int MaxBuffCount = 1000;

        /// <summary>
        /// The time delta between the last two ticks.
        /// </summary>
        [SerializeField] private float tickDelta;

        /// <summary>
        /// The current number of buffs in the group.
        /// </summary>
        [SerializeField] private int buffCount;

        /// <summary>
        /// The time of the last tick.
        /// </summary>
        [SerializeField] private float lastTickTime;

        /// <summary>
        /// Event invoked for each buff on every tick, passing the tick delta.
        /// </summary>
        private event Action<float> OnBuffTick;

        /// <summary>
        /// Gets the current number of buffs in the group.
        /// </summary>
        public int BuffCount => buffCount;

        /// <summary>
        /// Gets whether the group has space for more buffs.
        /// </summary>
        public bool HasSpace => buffCount < MaxBuffCount;

        /// <summary>
        /// Gets whether the group is full.
        /// </summary>
        public bool IsFull => !HasSpace;

        /// <summary>
        /// Registers a buff to this group and subscribes it to ticking.
        /// </summary>
        /// <param name="buff">The buff to register.</param>
        /// <returns>True if the buff was registered; false if the group is full.</returns>
        public bool RegisterBuff([NotNull] IBuff buff)
        {
            // Check if the buff is already registered in another group or if the group is full.
            if (IsFull || buff.BuffGroup != null) return false;
            OnBuffTick += buff.OnBuffTick;
            buff.BuffGroup = this;
            buffCount++;
            return true;
        }

        /// <summary>
        /// Unregisters a buff from this group and unsubscribes it from ticking.
        /// </summary>
        /// <param name="buff">The buff to unregister.</param>
        public void UnregisterBuff([NotNull] IBuff buff)
        {
            OnBuffTick -= buff.OnBuffTick;
            buffCount--;
        }

        /// <summary>
        /// Ticks all buffs in the group, passing the time since the last tick.
        /// </summary>
        internal void Tick()
        {
            float now = Time.time;
            tickDelta = now - lastTickTime;
            lastTickTime = now;
            // Invoke tick for all registered buffs with the calculated delta.
            OnBuffTick?.Invoke(tickDelta);
        }
    }
}