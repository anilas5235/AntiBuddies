using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    /// <summary>
    /// Handles pickup and attraction logic for resource objects within a certain range.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class ResourcePickUp : MonoBehaviour, INeedStatGroup
    {
        /// <summary>
        /// Reference to the stat controlling the attraction range.
        /// </summary>
        [SerializeField] private ValueStatRef attractRange;

        /// <summary>
        /// The collider used to detect objects within the attraction range.
        /// </summary>
        [SerializeField] private CircleCollider2D circleCollider;

        private void OnEnable()
        {
            attractRange.OnValueChange += UpdateColliderRadius;
            UpdateColliderRadius();
        }

        private void OnDisable()
        {
            attractRange.OnValueChange -= UpdateColliderRadius;
        }

        /// <summary>
        /// Updates the collider's radius to match the current attraction range stat.
        /// </summary>
        private void UpdateColliderRadius()
        {
            if (circleCollider)
            {
                circleCollider.radius = attractRange.CurrValue;
            }
        }

        /// <summary>
        /// Handles logic when an object enters the inner pickup range.
        /// </summary>
        /// <param name="contact">The contacting GameObject.</param>
        public void HandleInnerContact(GameObject contact)
        {
            if (!contact.CompareTag("PickUp")) return;
            if (contact.TryGetComponent(out IPickUpable pickUp))
            {
                pickUp.PickUp();
            }
        }

        /// <summary>
        /// Handles logic when an object enters the outer attraction range.
        /// </summary>
        /// <param name="contact">The contacting GameObject.</param>
        public void HandleOuterContact(GameObject contact)
        {
            if (!contact.CompareTag("PickUp")) return;
            if (contact.TryGetComponent(out IAttractable attractable))
            {
                attractable.AttractTo(gameObject);
            }
        }

        private void OnValidate()
        {
            attractRange.UpdateValue();
            UpdateColliderRadius();
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            // Initialize the attract range reference from the stat component.
            attractRange.OnStatInit(statGroup);
            if (circleCollider)
            {
                UpdateColliderRadius();
            }
        }
    }
}