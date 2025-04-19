using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack
{
    public abstract class AttackBehaviour : ScriptableObject
    {
        public bool IsPerforming => coroutine != null;
        public bool IsReady => coroutine == null;
        protected Coroutine coroutine;
        public abstract void PerformAttack(Weapon weapon);
    }
}