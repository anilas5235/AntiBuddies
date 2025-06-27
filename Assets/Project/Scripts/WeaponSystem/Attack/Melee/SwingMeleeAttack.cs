using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    /// <summary>
    /// Implements a swinging melee attack behaviour.
    /// </summary>
    [CreateAssetMenu(fileName = "SwingAttack", menuName = "WeaponSystem/Attacks/SwingAttack")]
    public class SwingMeleeAttack : MeleeAttackBehaviour
    {
        /// <summary>
        /// The maximum angle (in degrees) of the swing arc.
        /// swings back and forth between -swingAngle and +swingAngle.
        /// </summary>
        [SerializeField] private float swingAngle = 45f;

        /// <inheritdoc/>
        public override void AttackUpdate(MeleeWeapon weapon, float attackDelta)
        {
            // AngleOffset oscillates between -swingAngle and +swingAngle to animate the swing
            weapon.AngleOffset = (Mathf.PingPong(attackDelta, 2) - 1) * swingAngle;
        }
    }
}