using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Utils
{
    public class ContactTrigger2D : MonoBehaviour
    {
        [SerializeField] private bool onlyOncePerObject = true;
        public UnityEvent<GameObject> onContactEnter;

        private readonly List<GameObject> _contacts = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleContact(other.gameObject);
        }

        private void OnDisable()
        {
            ClearContacts();
        }

        private void HandleContact(GameObject other)
        {
            if (!other || other == gameObject) return;
            if (onlyOncePerObject)
            {
                if (_contacts.Contains(other)) return;
                _contacts.Add(other);
            }

            onContactEnter?.Invoke(other);
        }

        public void ClearContacts()
        {
            _contacts.Clear();
        }
    }
}