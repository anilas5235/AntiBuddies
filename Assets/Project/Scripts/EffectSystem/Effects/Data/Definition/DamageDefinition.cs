using System;
using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Data.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    /// <summary>
    /// Defines the data required to create a damage effect, including type and stat dependencies.
    /// </summary>
    [Serializable]
    public class DamageDefinition : EffectDefinition
    {
        /// <summary>
        /// The type of damage this effect deals.
        /// </summary>
        [SerializeField] private DamageType damageType;

        /// <summary>
        /// Additional stat dependencies that can influence the damage calculation.
        /// </summary>
        [SerializeField] private List<StatDependency> extraStatDeps;

        /// <summary>
        /// Creates a <see cref="DamagePackage"/> using the definition and provided source/stat context.
        /// </summary>
        /// <param name="source">The GameObject that is the source of the damage.</param>
        /// <param name="statComponent">The stat group to pull the scaling stats from.</param>
        /// <returns>A new <see cref="DamagePackage"/> instance.</returns>
        public DamagePackage CreatePackage(GameObject source, IStatGroup statComponent)
        {
            int finalAmount = amount;
            if (statComponent != null)
            {
                // Scale the base amount using the damage type and extra stat dependencies
                finalAmount = damageType.CreationScale(amount, statComponent, extraStatDeps);
            }

            return new DamagePackage(finalAmount, source, damageType);
        }
    }
}