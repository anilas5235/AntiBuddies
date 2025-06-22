using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    /// <summary>
    /// Represents a package containing all data required to apply a healing effect.
    /// </summary>
    [Serializable]
    public class HealPackage : EffectPackage
    {
        /// <summary>
        /// The type of healing this package represents.
        /// </summary>
        [SerializeField] private HealType healType;
       
        /// <param name="amount">The amount of healing.</param>
        /// <param name="healType">The type of healing.</param>
        public HealPackage(int amount, HealType healType) : base(amount)
        {
            this.healType = healType;
        }
        
        /// <summary>
        /// Gets the type of healing.
        /// </summary>
        public HealType HealType => healType;
    }
}