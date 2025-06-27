using UnityEngine;

namespace Project.Scripts.WeaponSystem.Targeting
{
    /// <summary>
    /// Abstract base class for targeting behaviours.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public abstract class TargetingBehaviour : ScriptableObject
    {
        /// <summary>
        /// Finds a target transform based on the provided location and range.
        /// </summary>
        /// <param name="location">The origin from which to search for targets.</param>
        /// <param name="range">The maximum range to search for targets.</param>
        /// <returns>The transform of the found target, or null if none found.</returns>
        public abstract Transform FindTarget(Transform location, float range);
    }
}