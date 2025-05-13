using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public interface IProjectile : IPoolable
    {
        public void SetData(ProjectileData projectileData);
        public void ProjectileSetUp(Vector2 direction, AlieGroup alieGroup, StatComponent statComponent, int contacts);
    }
}