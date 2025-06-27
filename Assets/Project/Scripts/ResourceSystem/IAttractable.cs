using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    /// <summary>
    /// Interface for objects that can be attracted to a target.
    /// </summary>
    public interface IAttractable
    {
        /// <summary>
        /// Attracts the object to the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The target GameObject to attract to.</param>
        /// <returns>True if the attraction was successful, false otherwise.</returns>
        bool AttractTo(GameObject gameObject);
    }
}