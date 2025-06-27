using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Range
{
    /// <summary>
    /// Abstract base class for ranged attack behaviours.
    /// </summary>
    public abstract class RangeAttackBehaviour : ScriptableObject
    {
        /// <summary>
        /// Gets the direction in which the ranged weapon should fire.
        /// </summary>
        /// <param name="weapon">The ranged weapon being used.</param>
        /// <returns>The direction vector for the attack.</returns>
        public abstract Vector2 GetDirection(RangeWeapon weapon);
    }
}