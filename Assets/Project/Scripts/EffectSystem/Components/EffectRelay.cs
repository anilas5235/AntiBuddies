using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage<AttackType>>, ITarget<EffectPackage<HealType>>, ITarget<EffectPackage<StatType>>, INeedStatComponent
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        private StatComponent _statComponent;
        public AlieGroup AlieGroup => alieGroup;

        public bool Apply(EffectPackage<AttackType> attackPackage)
        {
            if (attackPackage.AlieGroup == alieGroup) return false;
            healthComponent.ApplyAttack(attackPackage);
            return true;
        }

        public bool Apply(EffectPackage<HealType> healPackage)
        {
            if (healPackage.AlieGroup == alieGroup) return false;
            healthComponent.ApplyHeal(healPackage);
            return true;
        }

        public bool Apply(EffectPackage<StatType> statPackage)
        {
            if (statPackage.AlieGroup == alieGroup) return false;
            _statComponent.ModifyStat(statPackage);
            return true;
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
        }
    }
}