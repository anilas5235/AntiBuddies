using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    /// <summary>
    /// Implements a stabbing melee attack behaviour.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CreateAssetMenu(fileName = "StabAttack", menuName = "WeaponSystem/Attacks/StabAttack")]
    public class StabMeleeAttack : MeleeAttackBehaviour
    {
        /// <inheritdoc/>
        public override void AttackUpdate(MeleeWeapon weapon, float attackDelta)
        {
            Transform weaponTransform = weapon.transform;
            // Calculate the target position for the stab based on weapon's range and direction
            Vector3 target = weaponTransform.right * weapon.Range;
            // t oscillates between 0 and 1 to animate the stab forward and back
            float t = Mathf.PingPong(attackDelta * 2f, 1f);
            weaponTransform.localPosition = target * t;
        }
    }
}