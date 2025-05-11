using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private ProjectileData data;
        [SerializeField] private float speed = 10f;
        [SerializeField] private int allowedContacts = 1;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ContactEffect effectComponent;

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

        public GameObjectPool<ProjectileData> Pool { get; set; }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Activate(ProjectileData projectileData)
        {
            data = projectileData;
            gameObject.SetActive(true);
            speed = data.speed;
            allowedContacts = 1;
            spriteRenderer.sprite = data.sprite;
            transform.localScale = data.scale;
            effectComponent.ClearAll();
            effectComponent.damageEffects.Add(projectileData.damageEffects);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            rb.linearVelocity = Vector2.zero;
            speed = 0;
            effectComponent.ClearAll();
            effectComponent.statComponent = null;
        }

        public void ReturnToPool()
        {
            Pool.AddToPool(this);
        }

        public void ProjectileSetUp(Vector2 direction, AlieGroup alieGroup, StatComponent statComponent, int contacts)
        {
            if (direction == Vector2.zero) return;
            direction = direction.normalized;
            transform.right = direction;
            rb.linearVelocity = direction * speed;
            effectComponent.alieGroup = alieGroup;
            effectComponent.statComponent = statComponent;
            allowedContacts = contacts;
        }
    }
}