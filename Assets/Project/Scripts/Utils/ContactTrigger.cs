using UnityEngine;

namespace Project.Scripts.Utils
{
    public abstract class ContactTrigger : MonoBehaviour
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