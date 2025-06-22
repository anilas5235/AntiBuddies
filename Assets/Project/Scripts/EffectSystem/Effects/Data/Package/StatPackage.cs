using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    /// <summary>
    /// Represents a package containing all data required to apply a stat modification effect.
    /// </summary>
    [Serializable]
    public class StatPackage : EffectPackage
    {
        /// <summary>
        /// The type of stat this package modifies.
        /// </summary>
        [SerializeField] private StatType statType;

        /// <summary>
        /// The modification operation to apply to the stat.
        /// </summary>
        [SerializeField] private StatModification statMod;

        /// <param name="amount">The amount to modify the stat by.</param>
        /// <param name="statType">The type of stat to modify.</param>
        /// <param name="statMod">The modification operation.</param>
        public StatPackage(int amount, StatType statType, StatModification statMod) : base(amount)
        {
            this.statType = statType;
            this.statMod = statMod;
        }

        /// <summary>
        /// Gets the type of stat to modify.
        /// </summary>
        public StatType StatType => statType;

        /// <summary>
        /// Gets the modification operation to apply.
        /// </summary>
        public StatModification StatMod => statMod;

        /// <summary>
        /// Specifies the type of modification to apply to a stat.
        /// </summary>
        public enum StatModification
        {
            BaseValue,
            TempValue,
            MaxValue,
            MinValue
        }

        /// <summary>
        /// Creates a new <see cref="StatPackage"/> with the amount inverted.
        /// </summary>
        /// <returns>The resulting <see cref="StatPackage"/> with negated amount.</returns>
        public StatPackage Inverse() => new(-Amount, StatType, statMod); // Negate amount for inverse effect
    }
}