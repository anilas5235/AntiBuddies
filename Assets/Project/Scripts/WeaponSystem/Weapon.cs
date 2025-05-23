using System;
using System.Collections;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.WeaponSystem.Slot;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private TargetingBehaviour targetingBehaviour;
        [SerializeField] protected AlieGroup alieGroup = AlieGroup.Player;
        
        [SerializeField] protected DamageDefinition damage = new();
        [SerializeField] protected DamageBuffData buff;
        
        [SerializeField] private ValueStatRef attackSpeedStat;
        [SerializeField] private ValueStatRef rangeStat;

        private Transform _target;
        private WeaponSlot _weaponSlot;
        protected bool SearchingForTarget = true;
        protected Coroutine Coroutine;
        protected StatComponent StatComponent;
        public float Range => rangeStat.CurrValue;
        protected float AttackSpeed => attackSpeedStat.CurrValue;

        protected virtual void OnEnable()
        {
            _weaponSlot = GetComponentInParent<WeaponSlot>();
            StatComponent = GetComponentInParent<StatComponent>();
            
            attackSpeedStat.Init(StatComponent);
            rangeStat.Init(StatComponent);
        }

        public void Attack()
        {
            if (Coroutine != null) return;
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
            if (_target && Vector3.Distance(transform.position, _target.position) > Range) _target = null;
            if(!_target && SearchingForTarget) _target = targetingBehaviour.FindTarget(transform, Range);
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