using System;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : ContactTrigger, IProjectile
    {
        [SerializeField] private ProjectileData data;
        [SerializeField] private float speed = 10f;
        [SerializeField] private int allowedContacts = 1;
        [SerializeField] private Rigidbody2D rb;
        
        private Vector2 _direction;

        private void HandleContact()
        {
            allowedContacts--;
            if (allowedContacts <= 0)
            {
                Deactivate();
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = _direction * speed;
        }

        public void Setup(int contacts, float projectileSpeed, Vector2 direction)
        {
            allowedContacts = contacts;
            speed = projectileSpeed;
            transform.right = direction;
        }

        public GameObjectPool<ProjectileData> Pool { get; set; }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Activate(ProjectileData data)
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            rb.linearVelocity = Vector2.zero;
            allowedContacts = 1;
            speed = 0;
            _direction = Vector2.zero;
        }

        public void ReturnToPool()
        { 
            Pool.AddToPool(this);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
            if (direction != Vector2.zero)
            {
                transform.right = direction;
            }
        }

        protected override void HandleContact(GameObject other)
        {
            throw new NotImplementedException();
        }
    }
}