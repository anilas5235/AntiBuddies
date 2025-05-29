using Project.Scripts.StatSystem.Stats;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public class ResourcePickUp : MonoBehaviour, IHandleContact
    {
        [SerializeField] private ValueStatRef attractRange;
        private int _pickUpLayer;

        private void Awake()
        {
            _pickUpLayer = LayerMask.GetMask("PickUp");
        }

        public void HandleContact(GameObject contact)
        {
            if (!contact.CompareTag("PickUp")) return;
            if (contact.TryGetComponent(out IPickUpable pickUpable))
            {
                pickUpable.PickUp();
            }
        }

        private void FixedUpdate()
        {
            Collider2D[] result = Physics2D.OverlapCircleAll(transform.position, attractRange.CurrValue, _pickUpLayer);
            foreach (Collider2D obj in result)
            {
                if (!obj || !obj.gameObject.activeInHierarchy) continue;

                if (obj.CompareTag("PickUp") && obj.TryGetComponent(out IPickUpable pickUpable))
                {
                    pickUpable.AttractTo(gameObject);
                }
            }
        }
        
        private void OnValidate()
        {
            attractRange.UpdateValue();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attractRange.CurrValue);
        }
#endif
    }
}