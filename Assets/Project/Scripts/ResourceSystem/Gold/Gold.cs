using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.ResourceSystem.Gold
{
    /// <summary>
    /// Represents a gold pickup object that can be attracted and collected by the player.
    /// </summary>
    public class Gold : PoolableMono, IPickUpable, IAttractable
    {
        /// <summary>
        /// Amount of gold this pickup represents.
        /// </summary>
        [SerializeField] private int amount = 1;

        /// <summary>
        /// The transform to which this gold is currently being attracted.
        /// </summary>
        private Transform _destTransform;

        /// <summary>
        /// Time since this gold started being attracted.
        /// </summary>
        private float _attractedSince;

        /// <summary>
        /// Base speed at which the gold is attracted.
        /// </summary>
        private const float AttractedSpeed = 10f;

        /// <summary>
        /// Maximum speed at which the gold can be attracted.
        /// </summary>
        private const float MaxAttractedSpeed = 100f;

        /// <inheritdoc/>
        public override void Reset()
        {
            amount = 1;
            _destTransform = null;
            _attractedSince = 0f;
        }

        /// <inheritdoc/>
        public void PickUp()
        {
            if (amount <= 0)
            {
                Debug.LogWarning("Cannot pick up gold with non-positive amount.");
                return;
            }

            ResourceManager.Instance.AddGold(amount);
            ExpManager.Instance.AddExp(amount);
            ReturnToPool();
        }

        private void FixedUpdate()
        {
            if (!_destTransform) return;

            _attractedSince += Time.fixedDeltaTime;

            // Move towards the destination at a speed that increases over time, up to a maximum.
            transform.Translate((_destTransform.position - transform.position).normalized *
                                (Time.fixedDeltaTime * Mathf.Min(MaxAttractedSpeed, AttractedSpeed * _attractedSince)));
        }

        /// <inheritdoc/>
        public bool AttractTo(GameObject destination)
        {
            if (!destination || _destTransform)
            {
                Debug.LogWarning("Cannot attract to null destination or already attracted.");
                return false;
            }

            _destTransform = destination.transform;
            return true;
        }
    }
}