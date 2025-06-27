using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Type
{
    /// <summary>
    /// Abstract base class for all effect types, providing a name and description.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public abstract class EffectType : ScriptableObject
    {
        /// <summary>
        /// The description of the effect type.
        /// </summary>
        [SerializeField] private string description = "no description jet";

        /// <summary>
        /// Gets the name of the effect type from the ScriptableObject.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the description of the effect type.
        /// </summary>
        public string Description => description;
    }
}