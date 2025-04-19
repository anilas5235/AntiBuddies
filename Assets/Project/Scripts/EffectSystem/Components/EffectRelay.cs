using System;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage>
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private StatusComponent statusComponent;
        [SerializeField] private ResistanceComponent resistanceComponent;
        [SerializeField] private AmplificationComponent amplificationComponent;

        public HealthComponent HealthComponent => healthComponent;
        public AlieGroup AlieGroup => alieGroup;

        public bool Apply(EffectPackage applyable)
        {
            if (applyable.AlieGroup == alieGroup) return false;

            switch (applyable.EffectType.EffectCategory)
            {
                case EffectCategory.Attack:
                    Attack(applyable);
                    break;
                case EffectCategory.Heal:
                    Heal(applyable);
                    break;
                case EffectCategory.Status:
                    Status(applyable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        private void Heal(EffectPackage healPackage)
        {
            int heal = healPackage.Amount;
            if (amplificationComponent && healPackage.EffectType.AffectedByPercentModifier) amplificationComponent.AmplifyEffect(heal, healPackage.EffectType);
            healthComponent.ApplyHeal(heal, healPackage.EffectType);
        }

        private void Attack(EffectPackage attackPackage)
        {
            int damage = attackPackage.Amount;
            if (resistanceComponent)
            {
                damage = resistanceComponent.ResistEffect(damage, attackPackage.EffectType);
            }
            healthComponent.ApplyAttack(damage, attackPackage.EffectType);
        }

        private void Status(EffectPackage statusPackage)
        {
            
        }
    }
}