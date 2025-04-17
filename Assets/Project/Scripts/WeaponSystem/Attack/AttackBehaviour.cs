using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack
{
    public abstract class AttackBehaviour : ScriptableObject
    {
        public bool IsPerforming => coroutine != null;
        protected Coroutine coroutine;
        public abstract void PerformAttack(Weapon weapon);
    }
}