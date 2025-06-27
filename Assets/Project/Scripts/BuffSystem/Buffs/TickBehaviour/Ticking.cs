using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    /// <summary>
    /// Implements tick behaviour where the buff is applied at regular intervals.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class Ticking : ITickBehaviour
    {
        /// <summary>
        /// Time accumulated since the last tick.
        /// </summary>
        private float _timeSinceLastTick;

        /// <summary>
        /// The interval in seconds between each tick.
        /// </summary>
        private readonly float _tickInterval;

        /// <param name="tickInterval">The interval in seconds between each tick.</param>
        public Ticking(float tickInterval)
        {
            _tickInterval = tickInterval;
        }

        /// <inheritdoc/>
        public void OnBuffTick(IBuff buff, float deltaTime)
        {
            // Accumulate time since the last tick.
            _timeSinceLastTick += deltaTime;
            if (!(_timeSinceLastTick >= _tickInterval)) return;

            // Calculate how many ticks should occur based on accumulated time.
            int num = Mathf.FloorToInt(_timeSinceLastTick / _tickInterval);
            for (int i = 0; i < num; i++)
            {
                buff.OnBuffApply();
            }

            // Retain leftover time that didn't complete a full interval.
            _timeSinceLastTick %= _tickInterval;
        }
    }
}