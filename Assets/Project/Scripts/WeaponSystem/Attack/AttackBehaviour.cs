using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack
{
    public abstract class AttackBehaviour : ScriptableObject
    {
        public abstract void PerformAttack(Weapon weapon,float range, float attackInterval);
    }
}