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

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_projectilePool) _projectilePool = GlobalPools.Instance.GetPoolFor(projectileData.prefab);
        }

        protected override IEnumerator AttackRoutine(float interval)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                IProjectile projectile = (IProjectile)_projectilePool.GetObject();
                DynamicProjectileSettings settings = new(
                    damage,
                    buff,
                    extraEffectHandler,
                    attackBehaviour.GetDirection(this),
                    alliedGroup,
                    0, // for future use
                    gameObject,
                    StatGroup
                );
                projectile.SetData(projectileData, settings);
                projectile.SetTransform(projectileSpawnPoint.position, transform.rotation);
            }

            yield return new WaitForSeconds(interval);
            Coroutine = null;
        }

        public Vector3 GetStraightShotDirection()
        {
            return transform.right;
        }
    }
}