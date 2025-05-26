using UnityEngine;

namespace Project.ResourceSystem
{
    public interface IPickUpable
    {
        void PickUp();
        void AttractTo(GameObject gameObject);
    }
}