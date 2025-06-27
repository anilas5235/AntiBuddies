using UnityEngine;

namespace Project.Scripts.WeaponSystem.Targeting
{
    /// <summary>
    /// Targeting behaviour that finds the closest enemy within a specified range.
    /// </summary>
    [CreateAssetMenu(fileName = "TargetClosest", menuName = "WeaponSystem/Targeting/TargetClosest")]
    public class TargetClosest : TargetingBehaviour
    {
        /// <summary>
        /// The layer mask used to filter potential targets.
        /// </summary>
        [SerializeField] private LayerMask layerMask;

        /// <inheritdoc/>
        public override Transform FindTarget(Transform location, float range)
        {
            // Find all colliders within the specified range and layer mask
            Collider2D[] results = Physics2D.OverlapCircleAll(location.position, range, layerMask);
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D collider in results)
            {
                // Only consider objects tagged as "Enemy"
                if (!collider.CompareTag("Enemy")) continue;
                float distance = Vector3.Distance(location.position, collider.transform.position);
                // Update the closest target if this one is nearer
                if (!(distance < closestDistance)) continue;
                closestDistance = distance;
                closestTarget = collider.transform;
            }

            return closestTarget;
        }
    }
}