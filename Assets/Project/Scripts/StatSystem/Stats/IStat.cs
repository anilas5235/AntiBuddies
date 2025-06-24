using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// Interface representing a stat in the stat system.
    /// </summary>
    public interface IStat
    {
        /// <summary>
        /// Event triggered when the stat value changes.
        /// </summary>
        public event Action OnStatChange;

        /// <summary>
        /// The current value of the stat (after all modifications).
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Indicates if the stat is represented as a percentage or a flat value.
        /// </summary>
        public bool IsPercentage { get; }

        /// <summary>
        /// The type of the stat.
        /// </summary>
        public StatType StatType { get; }

        /// <summary>
        /// The raw value before clamping.
        /// </summary>
        public int FreeValue { get; }

        /// <summary>
        /// The maximum allowed value for this stat.
        /// </summary>
        public int MaxValue { get; }

        /// <summary>
        /// The minimum allowed value for this stat.
        /// </summary>
        public int MinValue { get; }

        /// <summary>
        /// Updates the stat's value, applying bonuses and clamping.
        /// </summary>
        public void UpdateValues();

        /// <summary>
        /// Resets the stat to its initial state.
        /// </summary>
        public void Reset();

        /// <summary>
        /// Resets only the temporary bonus of the stat.
        /// </summary>
        public void ResetTempStat();

        /// <summary>
        /// Modifies the stat using a StatPackage.
        /// </summary>
        /// <param name="package">The package describing the modification.</param>
        public void ModifyStat(StatPackage package);
    }
}