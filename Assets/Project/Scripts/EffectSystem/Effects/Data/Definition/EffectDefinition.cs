using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    /// <summary>
    /// Base class for all effect definitions, providing a common amount field.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public abstract class EffectDefinition
    {
        /// <summary>
        /// The base value or magnitude of the effect.
        /// </summary>
        [SerializeField] protected int amount;
    }
}