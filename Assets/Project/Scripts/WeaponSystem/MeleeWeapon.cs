using System.Collections;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Utils;
using Project.Scripts.WeaponSystem.Attack.Melee;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// Represents a melee weapon with attack behaviour and contact handling.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class MeleeWeapon : Weapon, IHandleContact
    {
        /// <summary>
        /// The behaviour that defines how this melee weapon attacks.
        /// </summary>
        [SerializeField] private MeleeAttackBehaviour meleeAttackBehaviour;

        /// <summary>
        /// The colliders used for the weapon's hit detection.
        /// </summary>
        [SerializeField] private Collider2D[] weaponColliders;

        /// <summary>
        /// The percentage of the attack interval spent in the rest phase.
        /// </summary>
        [SerializeField] private int restPercent = 30;

        /// <summary>
        /// The trigger used for contact detection.
        /// </summary>
        [SerializeField] private ContactTrigger2D ContactTrigger2D;

        /// <summary>
        /// The current angle offset applied to the weapon.
        /// </summary>
        internal float AngleOffset { get; set; }

        /// <summary>
        /// The rest phase as a fraction of the total interval.
        /// </summary>
        private float RestPercentage => restPercent / 100f;

        private void Awake()
        {
            SetColliderEnabled(false);
        }

        /// <summary>
        /// Enables or disables all weapon colliders and the contact trigger.
        /// </summary>
        /// <param name="state">True to enable, false to disable.</param>
        private void SetColliderEnabled(bool state)
        {
            ContactTrigger2D.enabled = state;
            foreach (Collider2D col in weaponColliders)
            {
                col.enabled = state;
            }
        }

        /// <inheritdoc/>
        public void HandleContact(GameObject contact)
        {
            ContactEffectProcessor hubAdapter = new(contact, alliedGroup, extraEffectHandler);
            hubAdapter.Apply(damage.CreatePackage(gameObject, StatGroup));
        }

        /// <inheritdoc/>
        protected override float CalculateAngleToTarget()
        {
            return base.CalculateAngleToTarget() + AngleOffset;
        }

        /// <inheritdoc/>
        protected override float CalcAttackInterval()
        {
            return AttackSpeed + Range / 10f;
        }

        /// <inheritdoc/>
        protected override IEnumerator AttackRoutine(float interval)
        {
            float elapsedTime = 0f;
            float restTime = interval * RestPercentage;
            float attackTime = interval - restTime;
            SetColliderEnabled(true);
            SearchingForTarget = false;

            // Move the weapon towards the target position during the attack phase
            while (elapsedTime < attackTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / attackTime;
                meleeAttackBehaviour.AttackUpdate(this, t);
                yield return null;
            }

            // Reset the weapon's transform and state
            transform.localPosition = Vector3.zero;
            AngleOffset = 0f;
            SearchingForTarget = true;
            // Deactivate the weapon collider
            SetColliderEnabled(false);
            // Wait for the rest of the attack interval (rest phase)
            yield return new WaitForSeconds(restTime);
            Coroutine = null;
        }
    }
}