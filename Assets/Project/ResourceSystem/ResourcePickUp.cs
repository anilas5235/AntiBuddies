using Project.Scripts.Utils;
using UnityEngine;

namespace Project.ResourceSystem
{
    public class ResourcePickUp : MonoBehaviour, IHandleContact
    {
        public void HandleContact(GameObject contact)
        {
            if(!contact.CompareTag("PickUp")) return;
            if (contact.TryGetComponent(out IPickUpable pickUpable))
            {
                pickUpable.PickUp();
            }
        }
    }
}