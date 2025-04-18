using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage>
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private StatusComponent statusComponent;
        [SerializeField] private HealingStats healingStats;
        [SerializeField] private ResistanceComponent resistanceComponent;
        
        public HealthComponent HealthComponent => healthComponent;
        public AlieGroup AlieGroup => alieGroup;
        public bool Apply(EffectPackage applyable)
        {
            if (applyable.AlieGroup == alieGroup)
            {
                return false;
            }
            
            switch (applyable.EffectType.EffectCategory)
            {
                case EffectCategory.Attack:
                    int damage = resistanceComponent.ResistEffect(applyable);
                    healthComponent.ApplyAttack(damage, applyable.EffectType);
                    break;
                case EffectCategory.Heal:
                    break;
                case EffectCategory.Status:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}
