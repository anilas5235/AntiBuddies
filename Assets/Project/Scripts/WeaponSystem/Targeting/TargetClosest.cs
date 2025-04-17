using UnityEngine;

namespace Project.Scripts.WeaponSystem.Targeting
{
    [CreateAssetMenu(fileName = "TargetClosest", menuName = "WeaponSystem/Targeting/TargetClosest")]
    public class TargetClosest : TargetingBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        public override Transform FindTarget(Transform location, float range)
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(location.position, range,layerMask);
            Transform closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider2D collider in results)
            {
                if (!collider.CompareTag("Enemy")) continue;
                float distance = Vector3.Distance(location.position, collider.transform.position);
                if (!(distance < closestDistance)) continue;
                closestDistance = distance;
                closestTarget = collider.transform;
            }

            return closestTarget;
        }
    }
}