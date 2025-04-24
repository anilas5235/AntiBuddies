using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Melee
{
    public abstract class MeleeAttackBehaviour : ScriptableObject
    {
        public abstract void AttackUpdate(MeleeWeapon weapon, float attackDelta);
    }
}