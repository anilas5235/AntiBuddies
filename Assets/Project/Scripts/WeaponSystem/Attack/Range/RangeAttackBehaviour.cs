using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack.Range
{
    public abstract class RangeAttackBehaviour : ScriptableObject
    {
        public abstract Vector2 GetDirection(RangeWeapon weapon);
    }
}