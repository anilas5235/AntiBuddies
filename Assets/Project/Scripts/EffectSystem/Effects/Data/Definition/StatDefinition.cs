using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    [Serializable]
    public record StatDefinition : EffectDefinition
    {
        [SerializeField] private StatType statType;
        [SerializeField] private StatPackage.StatModification statMod;

        public StatPackage CreatePackage()
        {
            return new StatPackage(amount, statType, statMod);
        }
    }
}