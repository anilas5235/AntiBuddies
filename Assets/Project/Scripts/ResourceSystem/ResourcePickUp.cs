using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public class ResourcePickUp : MonoBehaviour
    {
        [SerializeField] private ValueStatRef attractRange;
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

        private void UpdateColliderRadius()
        {
            if (circleCollider)
            {
                circleCollider.radius = attractRange.CurrValue;
            }
        }

        public void HandleInnerContact(GameObject contact)
        {
            if (!contact.CompareTag("PickUp")) return;
            if (contact.TryGetComponent(out IPickUpable pickUp))
            {
                pickUp.PickUp();
            }
        }

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
    }
}