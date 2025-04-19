using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class EffectSource : MonoBehaviour
    {
        [SerializeField] private EffectData effectData;
        [SerializeField] protected AlieGroup alieGroup;

        protected void ApplyEffect(ITarget<EffectPackage> target)
        {
            target?.Apply(effectData.GetPackage(gameObject, alieGroup));
        }
    }
}