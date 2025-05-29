using System.Collections;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.WeaponSystem.Attack.Range;
using Project.Scripts.WeaponSystem.Projectile;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class RangeWeapon : Weapon
    {
        private static GameObjectPool _projectilePool;

        [SerializeField] private RangeAttackBehaviour attackBehaviour;
        [SerializeField] private ProjectileData projectileData;
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private Transform projectileSpawnPoint;
        
        internal Transform ProjectileSpawnPoint => projectileSpawnPoint;
        protected override void OnEnable()
        {
            base.OnEnable();
            _projectilePool ??= GlobalPools.Instance.GetPoolFor(projectileData.prefab);
        }

        protected override IEnumerator AttackRoutine(float interval)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                IProjectile projectile = (IProjectile)_projectilePool.GetObject();
                projectile.SetData(projectileData, damage.CreatePackage(gameObject, StatComponent),
                    buff?.GetBuff(null, gameObject, StatComponent));
                projectile.SetTransform(projectileSpawnPoint.position, transform.rotation);
                projectile.ProjectileSetUp(attackBehaviour.GetDirection(this) * FlipMultiplier,
                    allyGroup, projectileData.contacts);
            }

            yield return new WaitForSeconds(interval);
            Coroutine = null;
        }
    }
}