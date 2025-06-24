using System.Collections;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.ExtraEffects;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using Project.Scripts.WeaponSystem.Slot;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// Abstract base class for all weapon types, providing common logic for targeting, attacking, and stat handling.
    /// </summary>
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        /// <summary>
        /// The targeting behaviour used to select targets.
        /// </summary>
        [SerializeField] private TargetingBehaviour targetingBehaviour;

        /// <summary>
        /// The allied group this weapon belongs to.
        /// </summary>
        [SerializeField] protected AlliedGroup alliedGroup = AlliedGroup.Player;

        /// <summary>
        /// Handler for extra effects applied on attack.
        /// </summary>
        [SerializeField] protected ExtraEffectHandler extraEffectHandler;

        /// <summary>
        /// The base damage definition for this weapon.
        /// </summary>
        [SerializeField] protected DamageDefinition damage = new();

        /// <summary>
        /// The buff data applied on attack.
        /// </summary>
        [SerializeField] protected DamageBuffData buff;

        /// <summary>
        /// The stat reference for attack speed.
        /// </summary>
        [SerializeField] private ValueStatRef attackSpeedStat;

        /// <summary>
        /// The stat reference for attack range.
        /// </summary>
        [SerializeField] private ValueStatRef rangeStat;

        /// <summary>
        /// The target that the weapon is currently attacking.
        /// </summary>
        private Transform _target;

        /// <summary>
        /// The weapon slot this weapon is attached to.
        /// </summary>
        private WeaponSlot _weaponSlot;

        /// <summary>
        /// Whether the weapon is currently searching for a target.
        /// </summary>
        protected bool SearchingForTarget = true;

        /// <summary>
        /// The coroutine running the attack routine.
        /// </summary>
        protected Coroutine Coroutine;

        /// <summary>
        /// The stat group used for stat calculations.
        /// </summary>
        protected IStatGroup StatGroup;

        /// <summary>
        /// The default angle of the weapon when not targeting anything.
        /// </summary>
        private float _defaultAngle;

        /// <summary>
        /// The current attack range of the weapon.
        /// </summary>
        public float Range => rangeStat.CurrValue;

        /// <summary>
        /// The current attack speed of the weapon.
        /// </summary>
        protected float AttackSpeed => attackSpeedStat.CurrValue;

        protected virtual void OnEnable()
        {
            _weaponSlot = GetComponentInParent<WeaponSlot>();
            _defaultAngle = _weaponSlot.GetDefaultWeaponAngle();
            StatGroup = GetComponentInParent<StatComponent>();

            attackSpeedStat.OnStatInit(StatGroup);
            rangeStat.OnStatInit(StatGroup);
        }

        /// <inheritdoc/>
        public void Attack()
        {
            if (Coroutine != null || !_target) return;
            Coroutine = StartCoroutine(AttackRoutine(CalcAttackInterval()));
        }

        /// <summary>
        /// Calculates the interval between attacks.
        /// </summary>
        /// <returns>The attack interval in seconds.</returns>
        protected virtual float CalcAttackInterval()
        {
            return attackSpeedStat.CurrValue;
        }

        private void FixedUpdate()
        {
            if (_target)
            {
                // If the target is too far away or inactive, search for a new target
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

        /// <summary>
        /// Searches for a new target using the targeting behaviour.
        /// </summary>
        private void SearchForTarget()
        {
            if (!SearchingForTarget) return;
            _target = targetingBehaviour.FindTarget(transform, Range);
        }

        /// <summary>
        /// Updates the weapon's rotation to face the current target.
        /// </summary>
        private void UpdateRotation()
        {
            // Rotate the weapon to face the target in 2D space => only rotate Z axis
            float angle = CalculateAngleToTarget();
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        /// <summary>
        /// Calculates the angle the weapon should face to target the current target.
        /// </summary>
        /// <returns>The angle in degrees.</returns>
        protected virtual float CalculateAngleToTarget()
        {
            if (!_target)
            {
                return Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, _defaultAngle, Time.deltaTime * 180f);
            }

            Vector3 direction = _weaponSlot.transform.InverseTransformPoint(_target.position);
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        /// <inheritdoc/>
        public void DestroyWeapon()
        {
            StopCoroutine(Coroutine);
            Destroy(gameObject);
        }

        /// <summary>
        /// Runs the attack routine for this weapon.
        /// </summary>
        /// <param name="interval">The interval between attacks.</param>
        /// <returns>An enumerator for the coroutine.</returns>
        protected abstract IEnumerator AttackRoutine(float interval);

        private void OnValidate()
        {
            // Ensure the attack speed and range stats are refreshed in the editor
            attackSpeedStat.UpdateValue();
            rangeStat.UpdateValue();
        }
    }
}