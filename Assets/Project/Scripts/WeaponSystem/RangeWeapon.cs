using System.Collections;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.WeaponSystem.Attack.Range;
using Project.Scripts.WeaponSystem.Projectile;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// Represents a ranged weapon that fires projectiles.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class RangeWeapon : Weapon
    {
        /// <summary>
        /// The pool used to reuse projectile instances.
        /// </summary>
        private static GameObjectPool _projectilePool;

        /// <summary>
        /// The behaviour that defines how this ranged weapon attacks.
        /// </summary>
        [SerializeField] private RangeAttackBehaviour attackBehaviour;

        /// <summary>
        /// The data for the projectile to be fired.
        /// </summary>
        [SerializeField] private ProjectileData projectileData;

        /// <summary>
        /// The number of projectiles fired per attack.
        /// </summary>
        [SerializeField] private int projectileCount = 1;

        /// <summary>
        /// The transform at which projectiles are spawned.
        /// </summary>
        [SerializeField] private Transform projectileSpawnPoint;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!_projectilePool) _projectilePool = GlobalPools.Instance.GetPoolFor(projectileData.prefab);
        }

        /// <inheritdoc/>
        protected override IEnumerator AttackRoutine(float interval)
        {
            for (int i = 0; i < projectileCount; i++)
            {
                IProjectile projectile = (IProjectile)_projectilePool.GetObject();
                // Create dynamic settings for the projectile
                DynamicProjectileSettings settings = new(
                    damage,
                    buff,
                    extraEffectHandler,
                    attackBehaviour.GetDirection(this),
                    alliedGroup,
                    0, // for future use
                    gameObject,
                    StatGroup,
                    true // fired by player
                );
                projectile.SetData(projectileData, settings);
                projectile.SetTransform(projectileSpawnPoint.position, transform.rotation);
            }

            yield return new WaitForSeconds(interval);
            Coroutine = null;
        }

        /// <summary>
        /// Gets the direction for a straight shot.
        /// </summary>
        /// <returns>The direction vector for a straight shot.</returns>
        public Vector3 GetStraightShotDirection()
        {
            return transform.right;
        }
    }
}