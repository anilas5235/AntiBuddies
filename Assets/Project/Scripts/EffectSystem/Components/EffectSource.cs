using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class EffectSource : MonoBehaviour
    {
        [SerializeField] private EffectData effectData;
        [SerializeField] protected AlieGroup alieGroup;

        protected void ApplyEffect(ITarget<EffectPackage> target)
        {
            if (!effectData){
                Debug.LogError("EffectData is not assigned in " + gameObject.name);
                return;
            }
            target?.Apply(effectData.GetPackage(gameObject, alieGroup));
        }
    }
}