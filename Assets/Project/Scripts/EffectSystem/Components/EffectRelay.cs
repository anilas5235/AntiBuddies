using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, IPackageTarget<DamageType>, IPackageTarget<HealType>,
        IPackageTarget<StatType>, INeedStatComponent
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        private StatComponent _statComponent;
        
        public void Apply(EffectPackage<DamageType> attackPackage)
        {
            healthComponent.Apply(attackPackage);
        }

        public void Apply(EffectPackage<HealType> healPackage)
        {
            healthComponent.Apply(healPackage);
        }

        public void Apply(EffectPackage<StatType> statPackage)
        {
            _statComponent.ModifyStat(statPackage);
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
        }

        public bool IsAlie(AlieGroup group) => group == alieGroup;
    }
}