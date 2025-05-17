using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public interface IProjectile : IPoolable
    {
        public void SetData(ProjectileData projectileData, IStatGroup statGroup, GameObject source);
        public void ProjectileSetUp(Vector2 direction, AlieGroup alieGroup, int contacts);
    }
}