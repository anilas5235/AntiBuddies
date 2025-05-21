using System.Collections;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.WeaponSystem.Attack.Melee;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private MeleeAttackBehaviour meleeAttackBehaviour;
        [SerializeField] private Collider2D[] weaponColliders;
        [SerializeField] private int restPercent = 30;
        [SerializeField] private ContactEffect contactEffect;
        internal float AngleOffset { get; set; }
        private float RestPercentage => restPercent / 100f;

        private void Awake()
        {
            SetColliderEnabled(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            contactEffect.statComponent = StatComponent;
        }

        private void SetColliderEnabled(bool b)
        {
            if(b) contactEffect.ClearContacts();
            foreach (Collider2D col in weaponColliders)
            {
                col.enabled = b;
            }
        }

        protected override float CalculateAngleToTarget()
        {
            return base.CalculateAngleToTarget() + AngleOffset;
        }

        protected override float CalcAttackInterval()
        {
            return AttackSpeed + Range/10f;
        }

        protected override IEnumerator AttackRoutine(float interval)
        {
            float elapsedTime = 0f;
            float restTime = interval * RestPercentage;
            float attackTime = interval - restTime;
            SetColliderEnabled(true);
            _searchingForTarget = false;

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
            _searchingForTarget = true;
            // deactivate the weapon collider
            SetColliderEnabled(false);
            // Wait for the rest of the attack interval
            yield return new WaitForSeconds(restTime);
            Coroutine = null;
        }
    }
}