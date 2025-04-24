using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    [CreateAssetMenu(fileName = "SwingAttack", menuName = "WeaponSystem/Attacks/SwingAttack")]

    public class SwingMeleeAttack : MeleeAttackBehaviour
    {
        [SerializeField] private float swingAngle = 45f;

        public override void AttackUpdate(MeleeWeapon weapon, float attackDelta)
        {
            Transform weaponTransform = weapon.transform;
            weapon.AngleOffset = (Mathf.PingPong(attackDelta, 2) - 1) * swingAngle;
        }
    }
}