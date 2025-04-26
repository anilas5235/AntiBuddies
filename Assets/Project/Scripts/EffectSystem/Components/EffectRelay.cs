using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage<AttackType>>, ITarget<EffectPackage<HealType>>,
        ITarget<EffectPackage<StatType>>
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private StatComponent statComponent;
        public AlieGroup AlieGroup => alieGroup;

        public bool Apply(EffectPackage<AttackType> attackPackage)
        {
            if (attackPackage.AlieGroup == alieGroup) return false;
            Attack(attackPackage);
            return true;
        }

        public bool Apply(EffectPackage<HealType> healPackage)
        {
            if (healPackage.AlieGroup == alieGroup) return false;
            Heal(healPackage);
            return true;
        }

        public bool Apply(EffectPackage<StatType> statPackage)
        {
            if (statPackage.AlieGroup == alieGroup) return false;
            Status(statPackage);
            return true;
        }

        private void Heal(EffectPackage<HealType> healPackage)
        {
            int heal = healPackage.Amount;
            healthComponent.ApplyHeal(heal, healPackage.EffectType);
        }

        private void Attack(EffectPackage<AttackType> attackPackage)
        {
            int damage = attackPackage.Amount;
            if (statComponent)
            {
                damage = statComponent.ResistAttack(damage, attackPackage.EffectType);
            }

            healthComponent.ApplyAttack(damage, attackPackage.EffectType);
        }

        private void Status(EffectPackage<StatType> statusP)
        {
            statComponent.IncreaseStat(statusP.Amount, statusP.EffectType);
        }
    }
}