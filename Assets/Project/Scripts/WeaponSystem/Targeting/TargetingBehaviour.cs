using UnityEngine;

namespace Project.Scripts.WeaponSystem.Targeting
{
    public abstract class TargetingBehaviour : ScriptableObject
    {
        public abstract Transform FindTarget(Transform location, float range);
    }
}