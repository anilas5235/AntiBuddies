using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class ContactEffectSource : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleContact(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HandleContact(other.gameObject);
        }

        protected abstract void HandleContact(GameObject other);
    }
}