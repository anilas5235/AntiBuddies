using System.Collections;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Attack
{
    [CreateAssetMenu(fileName = "StabAttack", menuName = "WeaponSystem/Attacks/StabAttack")]
    public class StabAttack : AttackBehaviour
    {
        [SerializeField] private float stabMagnitude = 1f;
        [SerializeField] private float stabDuration = 0.5f;
        public override void PerformAttack(Weapon weapon)
        {
            if(IsPerforming || !weapon) return;
            coroutine = weapon.StartCoroutine(StabAttackCoroutine(weapon.transform,stabMagnitude, stabDuration));
        }

        private IEnumerator StabAttackCoroutine(Transform weaponTransform ,float magnitude, float duration)
        {
            // Calculate the target position
            Vector3 target =weaponTransform.right * magnitude;

            // Calculate the time to move
            float elapsedTime = 0f;
            float halfDuration = duration / 2f;

            // Move the weapon towards the target position
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / halfDuration);
                t += Mathf.Min(halfDuration - elapsedTime,0f);
                weaponTransform.localPosition = Vector3.Lerp(Vector3.zero, target, t);

                yield return null;
            }

            // Reset the weapon's position
            weaponTransform.localPosition = Vector3.zero;
            coroutine = null;
        }
    }
}