using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public interface IPickUpable
    {
        void PickUp();
        void AttractTo(GameObject gameObject);
    }
}