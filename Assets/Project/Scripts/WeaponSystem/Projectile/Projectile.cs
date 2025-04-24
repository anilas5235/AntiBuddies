using System;
using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private int allowedContacts = 1;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ContactEffectSource contactEffectSource;

        private void OnEnable()
        {
            contactEffectSource.OnEffectApplied += HandleContact;
        }

        private void OnDisable()
        {
            contactEffectSource.OnEffectApplied -= HandleContact;
        }

        private void HandleContact()
        {
            allowedContacts--;
            if (allowedContacts <= 0)
            {
                DestroyProjectile();
            }
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = transform.right * speed;
        }

        public void Setup(int contacts, float projectileSpeed, Vector2 direction)
        {
            allowedContacts = contacts;
            speed = projectileSpeed;
            transform.right = direction;
        }

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}