using System;
using System.Collections;
using Project.Scripts.WeaponSystem.Slot;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private float range = 10f;
        [SerializeField] private float attackInterval = 1f;
        [SerializeField] private TargetingBehaviour targetingBehaviour;

        private Transform _target;
        private WeaponSlot _weaponSlot;
        protected Coroutine _coroutine;
        public float Range => range;

        protected virtual void OnEnable()
        {
            _weaponSlot = GetComponentInParent<WeaponSlot>();
        }


        public void Attack()
        {
            if (_coroutine != null) return;
            _coroutine = StartCoroutine(AttackRoutine(attackInterval));
        }


        private void FixedUpdate()
        {
            if (_target && Vector3.Distance(transform.position, _target.position) > range) _target = null;
            _target ??= targetingBehaviour.FindTarget(transform, range);
            if (!_target) return;
            UpdateRotation();
            Attack();
        }

        private void UpdateRotation()
        {
            // Rotate the weapon to face the target in 2D space => only rotate Z axis
            transform.localRotation = Quaternion.Euler(0, 0, CalculateAngleToTarget());
        }

        protected virtual float CalculateAngleToTarget()
        {
            Vector3 direction = _weaponSlot.transform.InverseTransformPoint(_target.position);
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public void DestroyWeapon()
        {
            Destroy(gameObject);
        }

        protected abstract IEnumerator AttackRoutine(float interval);
    }
}