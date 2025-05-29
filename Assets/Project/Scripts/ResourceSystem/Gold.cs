using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public class Gold : PoolableMono, IPickUpable
    {
        [SerializeField] private int amount = 1;
        private Transform _destTransform;
        private float _attractedSince;
        private const float AttractedSpeed = 10f;
        private const float MaxAttractedSpeed = 100f;

        public override void Reset()
        {
            amount = 1;
            _destTransform = null;
            _attractedSince = 0f;
        }

        public void PickUp()
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Cannot pick up gold with non-positive amount.");
                return;
            }

            ResourceManager.Instance.AddGold(amount);
            ReturnToPool();
        }

        private void FixedUpdate()
        {
            if (!_destTransform) return;

            _attractedSince += Time.fixedDeltaTime;

            transform.Translate((_destTransform.position - transform.position).normalized *
                                (Time.fixedDeltaTime * Mathf.Min(MaxAttractedSpeed, AttractedSpeed * _attractedSince)));
        }

        public void AttractTo(GameObject destination)
        {
            _destTransform ??= destination.transform;
        }
    }
}