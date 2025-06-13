using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    [CreateAssetMenu(fileName = "StabAttack", menuName = "WeaponSystem/Attacks/StabAttack")]
    public class StabMeleeAttack : MeleeAttackBehaviour
    {
        public override void AttackUpdate(MeleeWeapon weapon,  float attackDelta)
        {
            Transform weaponTransform = weapon.transform;
            Vector3 target = weaponTransform.right * (weapon.Range * weapon.FlipMultiplier);
            float t = Mathf.PingPong(attackDelta*2f, 1f);
            weaponTransform.localPosition = target * t;
        }
    }
}