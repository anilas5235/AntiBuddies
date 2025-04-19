using System;
using System.Collections;
using Project.Scripts.WeaponSystem.Attack;
using Project.Scripts.WeaponSystem.Slot;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private TargetingBehaviour targetingBehaviour;
        [SerializeField] private AttackBehaviour attackBehaviour;
        [SerializeField] private float range = 10f;
        [SerializeField] private float attacksPerSecond = 3f;
        [SerializeField] private Collider2D[] weaponColliders;
        private Transform _target;
        private WeaponSlot _weaponSlot;
        private float AttackInterval => 1f / attacksPerSecond;
        public void SetWeaponSlot(WeaponSlot weaponSlot) => _weaponSlot = weaponSlot;
        
        private bool IsPerforming => !IsReady;
        private bool IsReady => _coroutine == null;
        private Coroutine _coroutine;

        private void Awake()
        {
            SetColliderEnabled(false);
        }

        public void Attack()
        {
            if (IsReady) attackBehaviour.PerformAttack(this, range, AttackInterval);
        }
        
        internal void StartAttackCoroutine(IEnumerator routine) => _coroutine = StartCoroutine(routine);
        internal void AttackCoroutineFinished() => _coroutine = null;
        internal void SetColliderEnabled(bool b)
        {
            foreach (Collider2D col in weaponColliders)
            {
                col.enabled = b;
            }
        }
        private void FixedUpdate()
        {
            if(_target && Vector3.Distance(transform.position, _target.position) > range) _target = null;
            _target ??= targetingBehaviour.FindTarget(transform, range);
            if (!_target) return;
            UpdateRotation();
            Attack();
        }

        private void UpdateRotation()
        {
            // Rotate the weapon to face the target in 2D space => only rotate Z axis
            Vector3 direction = _weaponSlot.transform.InverseTransformPoint(_target.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        public void DestroyWeapon()
        {
            Destroy(gameObject);
        }
    }
}