using System.Collections;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.WeaponSystem.Attack.Range;
using Project.Scripts.WeaponSystem.Projectile;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private RangeAttackBehaviour attackBehaviour;
        [SerializeField] private ProjectileData projectileData;
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private bool isFlip;
        internal Transform ProjectileSpawnPoint => projectileSpawnPoint;
        private float FlipMultiplier => isFlip ? -1 : 1;

        protected override void UpdateRotation()
        {
            float angle = CalculateAngleToTarget();
            isFlip = Mathf.Abs(angle) > 90;
            if (isFlip) angle -= 180;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(FlipMultiplier, 1, 1);
        }

        protected override IEnumerator AttackRoutine(float interval)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                IProjectile projectile = (IProjectile) GlobalPools.Instance.ProjectilePool.GetObject();
                projectile.Activate(projectileData);
                projectile.SetTransform(projectileSpawnPoint.position, transform.rotation);
                projectile.SetDirection(attackBehaviour.GetDirection(this) * FlipMultiplier);
            }

            yield return new WaitForSeconds(interval);
            _coroutine = null;
        }
    }
}