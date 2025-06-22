using System.Collections;
using Project.Scripts.Utils;
using Project.Scripts.WeaponSystem.Attack.Melee;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class MeleeWeapon : Weapon, IHandleContact
    {
        [SerializeField] private MeleeAttackBehaviour meleeAttackBehaviour;
        [SerializeField] private Collider2D[] weaponColliders;
        [SerializeField] private int restPercent = 30;
        [SerializeField] private ContactTrigger2D ContactTrigger2D;
        internal float AngleOffset { get; set; }
        private float RestPercentage => restPercent / 100f;

        private void Awake()
        {
            SetColliderEnabled(false);
        }

        private void SetColliderEnabled(bool b)
        {
            ContactTrigger2D.enabled = b;
            foreach (Collider2D col in weaponColliders)
            {
                col.enabled = b;
            }
        }

        public void HandleContact(GameObject contact)
        {
            ContactToHubAdapter hubAdapter = new(contact, alliedGroup, extraEffectHandler);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(damage.CreatePackage(gameObject, StatComponent));
        }

        protected override float CalculateAngleToTarget()
        {
            return base.CalculateAngleToTarget() + AngleOffset;
        }

        protected override float CalcAttackInterval()
        {
            return AttackSpeed + Range / 10f;
        }

        protected override IEnumerator AttackRoutine(float interval)
        {
            float elapsedTime = 0f;
            float restTime = interval * RestPercentage;
            float attackTime = interval - restTime;
            SetColliderEnabled(true);
            SearchingForTarget = false;

            // Move the weapon towards the target position
            while (elapsedTime < attackTime)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / attackTime;
                meleeAttackBehaviour.AttackUpdate(this, t);
                yield return null;
            }

            // Reset the weapon's transform
            transform.localPosition = Vector3.zero;
            AngleOffset = 0f;
            SearchingForTarget = true;
            // deactivate the weapon collider
            SetColliderEnabled(false);
            // Wait for the rest of the attack interval
            yield return new WaitForSeconds(restTime);
            Coroutine = null;
        }
    }
}