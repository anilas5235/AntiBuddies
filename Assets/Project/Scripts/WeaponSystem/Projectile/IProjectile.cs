using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public interface IProjectile : IPoolable<ProjectileData>
    {
        public void SetDirection(Vector2 direction);
        public void SetAlieGroup(AlieGroup alieGroup);
    }
}