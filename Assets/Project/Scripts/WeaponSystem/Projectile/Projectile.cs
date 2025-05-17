using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : PoolableMono, IProjectile
    {
        [SerializeField] private ProjectileData data;
        [SerializeField] private float speed = 10f;
        [SerializeField] private int allowedContacts = 1;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private StaticContactEffect effectComponent;

        private void OnEnable()
        {
            effectComponent.OnEffectApplied += HandleContact;
        }

        private void OnDisable()
        {
            effectComponent.OnEffectApplied -= HandleContact;
        }

        private void HandleContact()
        {
            allowedContacts--;
            if (allowedContacts <= 0)
            {
                ReturnToPool();
            }
        }

        public override void Reset()
        {
            rb.linearVelocity = Vector2.zero;
            speed = 0;
            effectComponent.ClearAll();
        }
       

        public void SetData(ProjectileData projectileData, IStatGroup statGroup, GameObject source)
        {
            data = projectileData;
            speed = data.speed;
            allowedContacts = 1;
            spriteRenderer.sprite = data.sprite;
            transform.localScale = data.scale;
            effectComponent.ClearAll();
            foreach (EffectDef<DamageType> effect in projectileData.damageEffects)
            {
                effectComponent.damageEffects.Add(effect.CreatePackage(source, statGroup));
            }
        }

        public void ProjectileSetUp(Vector2 direction, AlieGroup alieGroup, int contacts)
        {
            if (direction == Vector2.zero) return;
            direction = direction.normalized;
            transform.right = direction;
            rb.linearVelocity = direction * speed;
            effectComponent.alieGroup = alieGroup;
            allowedContacts = contacts;
        }
    }
}