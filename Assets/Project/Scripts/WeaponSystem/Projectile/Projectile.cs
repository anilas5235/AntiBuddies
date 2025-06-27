using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    /// <summary>
    /// Represents a projectile that can be fired and interact with other objects.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : PoolableMono, IProjectile
    {
        /// <summary>
        /// The configuration data for this projectile.
        /// </summary>
        [SerializeField] private ProjectileData data;

        /// <summary>
        /// The number of allowed contacts before the projectile is destroyed.
        /// </summary>
        [SerializeField] private int allowedContacts = 1;

        /// <summary>
        /// The sprite renderer for the projectile's appearance.
        /// </summary>
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <summary>
        /// The Rigidbody2D component for physics interactions.
        /// </summary>
        [SerializeField] private Rigidbody2D rb;

        /// <summary>
        /// The dynamic runtime settings for this projectile, including direction, damage, buffs, and effects.
        /// </summary>
        private DynamicProjectileSettings _dynamicData;

        /// <inheritdoc/>
        public override void Reset()
        {
            // Reset projectile's velocity and clear references
            rb.linearVelocity = Vector2.zero;
            data = null;
            _dynamicData = null;
        }

        /// <inheritdoc/>
        public void HandleContact(GameObject contact)
        {
            if(_dynamicData == null) return;
            // Try to apply effects to the contact; if not valid or allied, do nothing
            if (!_dynamicData.ApplyEffects(contact))
            {
                return;
            }

            allowedContacts--;
            // Return to pool if no contacts remain
            if (allowedContacts <= 0)
            {
                ReturnToPool();
            }
        }

        /// <inheritdoc/>
        public void SetData(ProjectileData projectileData, DynamicProjectileSettings settings)
        {
            data = projectileData;
            _dynamicData = settings;
            spriteRenderer.sprite = data.sprite;
            transform.localScale = data.scale;
            Vector2 direction = settings.Direction.normalized;
            // Set the projectile's facing direction
            transform.right = direction;
            // Set the projectile's velocity
            rb.linearVelocity = direction * data.speed;
            // Add any additional allowed contacts from dynamic settings
            allowedContacts = data.contacts + settings.AdditionalContacts;
        }
    }
}