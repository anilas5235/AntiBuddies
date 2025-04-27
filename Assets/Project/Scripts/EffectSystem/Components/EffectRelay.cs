using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, ITarget<EffectPackage<AttackType>>, ITarget<EffectPackage<HealType>>
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private PlayerStats playerStats;
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
    }
}