using System.Collections;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.WeaponSystem.Slot;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.WeaponSystem
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private TargetingBehaviour targetingBehaviour;
        [SerializeField] protected AlliedGroup alliedGroup = AlliedGroup.Player;
        [SerializeField] protected ExtraEffectHandler extraEffectHandler;

        [SerializeField] protected DamageDefinition damage = new();
        [SerializeField] protected DamageBuffData buff;

        [SerializeField] private ValueStatRef attackSpeedStat;
        [SerializeField] private ValueStatRef rangeStat;

        private Transform _target;
        private WeaponSlot _weaponSlot;
        protected bool SearchingForTarget = true;
        protected Coroutine Coroutine;
        protected IStatGroup StatGroup;
        private float _defaultAngle;
        public float Range => rangeStat.CurrValue;
        protected float AttackSpeed => attackSpeedStat.CurrValue;

        protected virtual void OnEnable()
        {
            _weaponSlot = GetComponentInParent<WeaponSlot>();
            _defaultAngle = _weaponSlot.GetDefaultWeaponAngle();
            StatGroup = GetComponentInParent<StatComponent>();

            attackSpeedStat.OnStatInit(StatGroup);
            rangeStat.OnStatInit(StatGroup);
        }

        public void Attack()
        {
            if (Coroutine != null || !_target) return;
            Coroutine = StartCoroutine(AttackRoutine(CalcAttackInterval()));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("OnTriggerEnter2D weapon " + other.name);
        }

        protected virtual float CalcAttackInterval()
        {
            return attackSpeedStat.CurrValue;
        }

        private void FixedUpdate()
        {
            if (_target)
            {
                if (Vector3.Distance(transform.position, _target.position) > Range ||
                    !_target.gameObject.activeInHierarchy) SearchForTarget();
            }
            else
            {
                SearchForTarget();
            }

            UpdateRotation();
            Attack();
        }

        private void SearchForTarget()
        {
            if (!SearchingForTarget) return;
            _target = targetingBehaviour.FindTarget(transform, Range);
        }

        private void UpdateRotation()
        {
            // Rotate the weapon to face the target in 2D space => only rotate Z axis
            float angle = CalculateAngleToTarget();
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        protected virtual float CalculateAngleToTarget()
        {
            if (!_target)
            {
                return Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, _defaultAngle, Time.deltaTime * 180f);
            }

            Vector3 direction = _weaponSlot.transform.InverseTransformPoint(_target.position);
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public void DestroyWeapon()
        {
            StopCoroutine(Coroutine);
            Destroy(gameObject);
        }

        protected abstract IEnumerator AttackRoutine(float interval);

        private void OnValidate()
        {
            attackSpeedStat.UpdateValue();
            rangeStat.UpdateValue();
        }
    }
}