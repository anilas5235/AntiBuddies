using System.Collections;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack
{
    [CreateAssetMenu(fileName = "StabAttack", menuName = "WeaponSystem/Attacks/StabAttack")]
    public class StabAttack : AttackBehaviour
    {
        [SerializeField] private int restPercent = 50;
        private float RestPercentage => restPercent / 100f;
        public override void PerformAttack(Weapon weapon,float range, float attackInterval)
        {
            if (!weapon) return;
            weapon.StartAttackCoroutine(StabAttackCoroutine(weapon, range, attackInterval));
        }

        private IEnumerator StabAttackCoroutine(Weapon weapon, float magnitude, float interval)
        {
            // Calculate the time to move
            float elapsedTime = 0f;
            float duration = interval * RestPercentage;
            float halfDuration = duration / 2f;
            Transform weaponTransform = weapon.transform;
            // activate the weapon collider
            weapon.SetColliderEnabled(true);
            
            // Move the weapon towards the target position
            while (elapsedTime < duration)
            {
                Vector3 target = weaponTransform.right * magnitude;
                elapsedTime += Time.deltaTime;
                float t = Mathf.PingPong(elapsedTime / halfDuration, 1f);
                weaponTransform.localPosition = target * t;

                yield return null;
            }

            // Reset the weapon's position
            weaponTransform.localPosition = Vector3.zero;
            // deactivate the weapon collider
            weapon.SetColliderEnabled(false);
            // Wait for the rest of the attack interval
            yield return new WaitForSeconds(interval-duration);
            weapon.AttackCoroutineFinished();
        }
    }
}