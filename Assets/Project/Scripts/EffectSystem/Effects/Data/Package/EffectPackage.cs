using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    /// <summary>
    /// Abstract base class for all effect packages, containing a common amount field.
    /// </summary>
    [Serializable]
    public abstract class EffectPackage
    {
        /// <summary>
        /// The value or magnitude of the effect.
        /// </summary>
        [SerializeField] private int amount;

        /// <summary>
        /// Gets the effect amount.
        /// </summary>
        public int Amount => amount;
       
        /// <param name="amount">The value or magnitude of the effect.</param>
        protected EffectPackage(int amount)
        {
            this.amount = amount;
        }
    }
}