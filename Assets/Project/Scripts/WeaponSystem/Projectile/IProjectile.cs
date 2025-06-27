using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;

namespace Project.Scripts.WeaponSystem.Projectile
{
    /// <summary>
    /// Interface for all projectile types.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IProjectile : IPoolable, IHandleContact
    {
        /// <summary>
        /// Sets the configuration and dynamic runtime settings for the projectile.
        /// </summary>
        /// <param name="projectileData">The projectile's static configuration data.</param>
        /// <param name="settings">The dynamic settings for the projectile, including direction, damage, buffs, and effects.</param>
        void SetData(ProjectileData projectileData, DynamicProjectileSettings settings);
    }
}