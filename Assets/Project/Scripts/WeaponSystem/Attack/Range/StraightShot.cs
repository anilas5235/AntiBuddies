using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Range
{
    /// <summary>
    /// Implements a straight shot ranged attack behaviour.
    /// </summary>
    [CreateAssetMenu(fileName = "StraightShot", menuName = "WeaponSystem/Attacks/StraightShot")]
    public class StraightShot : RangeAttackBehaviour
    {
        /// <inheritdoc/>
        public override Vector2 GetDirection(RangeWeapon weapon) => weapon.GetStraightShotDirection();
    }
}