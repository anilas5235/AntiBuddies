using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    /// <summary>
    /// Abstract base class for melee attack behaviours.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public abstract class MeleeAttackBehaviour : ScriptableObject
    {
        /// <summary>
        /// Updates the melee attack state for the given weapon.
        /// </summary>
        /// <param name="weapon">The melee weapon being used.</param>
        /// <param name="attackDelta">Progress of the attack, a value between 0 and 1.</param>
        public abstract void AttackUpdate(MeleeWeapon weapon, float attackDelta);
    }
}