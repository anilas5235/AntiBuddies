using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IEffectSource
    {
        public void Attack(GameObject target);
    }
}