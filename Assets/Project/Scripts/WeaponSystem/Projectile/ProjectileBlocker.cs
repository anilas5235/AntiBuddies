using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    public class ProjectileBlocker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IProjectile projectile = other.GetComponentInParent<IProjectile>();
            projectile?.DestroyProjectile();
        }
    }
}