using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    /// <summary>
    /// Defines the data required to create a stat modification effect.
    /// </summary>
    [Serializable]
    public class StatDefinition : EffectDefinition
    {
        /// <summary>
        /// The type of stat this effect modifies.
        /// </summary>
        [SerializeField] private StatType statType;

        /// <summary>
        /// The modification operation to apply to the stat.
        /// </summary>
        [SerializeField] private StatPackage.StatModification statMod;

        /// <summary>
        /// Creates a <see cref="StatPackage"/> using the definition.
        /// </summary>
        /// <returns>A new <see cref="StatPackage"/> instance.</returns>
        public StatPackage CreatePackage()
        {
            return new StatPackage(amount, statType, statMod);
        }
    }
}