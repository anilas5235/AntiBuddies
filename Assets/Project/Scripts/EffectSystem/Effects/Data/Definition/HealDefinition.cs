using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Data.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    /// <summary>
    /// Defines the data required to create a healing effect.
    /// <remarks>Author: Niklas Borchers</remarks>
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public class HealDefinition : EffectDefinition
    {
        /// <summary>
        /// The type of healing this effect provides.
        /// </summary>
        [SerializeField] private HealType healType;

        /// <summary>
        /// Creates a <see cref="HealPackage"/> using the definition.
        /// </summary>
        /// <returns>A new <see cref="HealPackage"/> instance.</returns>
        public HealPackage CreatePackage()
        {
            return new HealPackage(amount, healType);
        }
    }
}