using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.ResourceSystem
{
    public class Gold : PoolableMono, IPickUpable
    {
        [SerializeField] private int amount = 1;

        public override void Reset()
        {
            amount = 1;
        }

        public void PickUp()
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Cannot pick up gold with non-positive amount.");
                return;
            }

            ResourceManager.Instance.AddGold(amount);
            Debug.Log($"Picked up {amount} gold. Total gold: {ResourceManager.Instance.Gold}");
            ReturnToPool();
        }
    }
}