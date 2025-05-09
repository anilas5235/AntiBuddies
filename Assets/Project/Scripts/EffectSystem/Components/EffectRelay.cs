using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage<DamageType>>, ITarget<EffectPackage<HealType>>, ITarget<EffectPackage<StatType>>, INeedStatComponent
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        private StatComponent _statComponent;
        public AlieGroup AlieGroup => alieGroup;

        public void Apply(EffectPackage<DamageType> attackPackage)
        {
            healthComponent.ApplyAttack(attackPackage);
        }

        public void Apply(EffectPackage<HealType> healPackage)
        {
            healthComponent.ApplyHeal(healPackage);
        }

        public void Apply(EffectPackage<StatType> statPackage)
        {
            _statComponent.ModifyStat(statPackage);
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
        }
    }
}