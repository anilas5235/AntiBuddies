using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public interface IEffectSource
    {
        public void ApplyTo(GameObject target);

        public GameObject GetGameObject();
    }
}