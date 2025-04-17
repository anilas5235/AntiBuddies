using System;
using Project.Scripts.WeaponSystem.Attack;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private TargetingBehaviour targetingBehaviour;
        [SerializeField] private AttackBehaviour attackBehaviour;
        private Transform target;

        public void Attack()
        {
            attackBehaviour.PerformAttack(this);
        }

        private void FixedUpdate()
        {
            target ??= targetingBehaviour.FindTarget(transform, 10f);
            if(target){
                Vector3 position = target.position;
                transform.LookAt(new Vector3(position.x, position.y, transform.position.z));
            }
        }

        public void DestroyWeapon()
        {
            Destroy(gameObject);
        }
    }
}