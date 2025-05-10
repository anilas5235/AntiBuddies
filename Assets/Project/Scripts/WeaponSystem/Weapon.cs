using System.Collections;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.StatSystem;
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
        [SerializeField] protected AlieGroup alieGroup;

        private Transform _target;
        private WeaponSlot _weaponSlot;
        protected Coroutine Coroutine;
        protected StatComponent StatComponent;
        public float Range => range;

        protected virtual void OnEnable()
        {
            _weaponSlot = GetComponentInParent<WeaponSlot>();
            StatComponent = GetComponentInParent<StatComponent>();
        }


        public void Attack()
        {
            if (Coroutine != null) return;
            Coroutine = StartCoroutine(AttackRoutine(attackInterval));
        }


        private void FixedUpdate()
        {
            if (_target && Vector3.Distance(transform.position, _target.position) > range) _target = null;
            _target ??= targetingBehaviour.FindTarget(transform, range);
            if (!_target) return;
            UpdateRotation();
            Attack();
        }

        protected virtual void UpdateRotation()
        {
            // Rotate the weapon to face the target in 2D space => only rotate Z axis
            float angle = CalculateAngleToTarget();
            transform.localRotation = Quaternion.Euler(0, 0, angle);
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