using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public interface IProjectile
    {
        public void Setup(int contacts, float projectileSpeed, Vector2 direction);
        public void DestroyProjectile();
    }
}