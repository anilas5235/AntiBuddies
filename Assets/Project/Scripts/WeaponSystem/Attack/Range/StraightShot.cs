using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Range
{
    [CreateAssetMenu(fileName = "StraightShot", menuName = "WeaponSystem/Attacks/StraightShot")]

    public class StraightShot : RangeAttackBehaviour
    {
        public override Vector2 GetDirection(RangeWeapon weapon) => weapon.ProjectileSpawnPoint.right;
    }
}