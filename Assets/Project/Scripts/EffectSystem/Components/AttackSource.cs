using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class AttackSource : MonoBehaviour
    {
        [SerializeField] private AttackData attackData;
        [SerializeField] protected AlieGroup alieGroup;
        
        public event Action OnEffectApplied; 

        private void Awake()
        {
            if (attackData) return;
            Debug.LogError("EffectData is not assigned in " + gameObject.name);
        }

        protected void Attack(ITarget<EffectPackage<AttackType>> target)
        {
            if (target == null) return;
            if(target.Apply(attackData.GetPackage(gameObject, alieGroup)))
            {
                OnEffectApplied?.Invoke();
            }
        }
    }
}