using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class EffectSource : MonoBehaviour
    {
        [SerializeField] private EffectData effectData;
        [SerializeField] protected AlieGroup alieGroup;
        
        public event Action OnEffectApplied; 

        private void Awake()
        {
            if (effectData) return;
            Debug.LogError("EffectData is not assigned in " + gameObject.name);
        }

        protected void ApplyEffect(ITarget<EffectPackage> target)
        {
            if (target == null) return;
            if(target.Apply(effectData.GetPackage(gameObject, alieGroup)))
            {
                OnEffectApplied?.Invoke();
            }
        }
    }
}