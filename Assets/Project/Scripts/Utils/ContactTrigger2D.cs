using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.Utils
{
    /// <summary>
    /// Triggers UnityEvents when other GameObjects enter its 2D collider, with optional per-object filtering.
    /// </summary>
    [RequireComponent(typeof(Collider2D)), DisallowMultipleComponent]
    public class ContactTrigger2D : MonoBehaviour
    {
        /// <summary>
        /// If true, only triggers once per unique object until cleared.
        /// </summary>
        [SerializeField] private bool onlyOncePerObject = true;

        /// <summary>
        /// Event invoked when a valid contact occurs.
        /// </summary>
        public UnityEvent<GameObject> onContactEnter;

        /// <summary>
        /// Tracks objects that have already triggered the event if onlyOncePerObject is true.
        /// </summary>
        private readonly List<GameObject> _contacts = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleContact(other.gameObject);
        }

        private void OnDisable()
        {
            ClearContacts();
        }

        /// <summary>
        /// Handles logic for invoking the contact event, respecting the onlyOncePerObject setting.
        /// </summary>
        /// <param name="other">The contacting GameObject.</param>
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

        /// <summary>
        /// Clears the list of contacted objects, allowing them to trigger again.
        /// </summary>
        public void ClearContacts()
        {
            _contacts.Clear();
        }
    }
}