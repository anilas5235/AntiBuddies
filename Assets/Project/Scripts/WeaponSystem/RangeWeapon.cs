using System.Collections;
using Project.Scripts.WeaponSystem.Attack.Range;
using Project.Scripts.WeaponSystem.Projectile;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private RangeAttackBehaviour attackBehaviour;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private Transform projectileSpawnPoint;
        internal Transform ProjectileSpawnPoint => projectileSpawnPoint;

        protected override IEnumerator AttackRoutine(float interval)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position,
                    transform.rotation);
                IProjectile projectileComp = projectile.GetComponent<IProjectile>();
                projectileComp.Setup(1, 10f, attackBehaviour.GetDirection(this));
            }
            yield return new WaitForSeconds(interval);
            _coroutine = null;
        }
    }
}