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
            if (target && !attackBehaviour.IsPerforming)
            {
                attackBehaviour.PerformAttack(this);
            }
        }

        private void FixedUpdate()
        {
            target ??= targetingBehaviour.FindTarget(transform, 10f);
            if (target)
            {
                // Rotate the weapon to face the target in 2D space => only rotate Z axis
                Vector3 direction = transform.InverseTransformPoint(target.position);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.Rotate(0,0, angle);
                
                Attack();
            }
        }

        public void DestroyWeapon()
        {
            Destroy(gameObject);
        }
    }
}