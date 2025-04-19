using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage>
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private ResistanceComponent resistance;
        [SerializeField] private AmplificationComponent amplification;
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
            if (amplification && healPackage.EffectType.AffectedByPercentModifier) amplification.AmplifyEffect(heal, healPackage.EffectType);
            healthComponent.ApplyHeal(heal, healPackage.EffectType);
        }

        private void Attack(EffectPackage attackPackage)
        {
            int damage = attackPackage.Amount;
            if (resistance)
            {
                damage = resistance.ResistEffect(damage, attackPackage.EffectType);
            }
            healthComponent.ApplyAttack(damage, attackPackage.EffectType);
        }

        private void Status(EffectPackage statusP)
        {
            if (amplification && amplification.IncreaseAmplifier(statusP.Amount,statusP.EffectType)) return;
            if (resistance) resistance.IncreaseResistance(statusP.Amount, statusP.EffectType);
        }
    }
}