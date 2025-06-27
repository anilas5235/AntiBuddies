using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    /// <summary>
    /// Blocks projectiles that enter its trigger collider.
    /// </summary>
    public class ProjectileBlocker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.isTrigger) return;
            IProjectile projectile = other.GetComponent<IProjectile>();
            projectile?.ReturnToPool();
        }
    }
}