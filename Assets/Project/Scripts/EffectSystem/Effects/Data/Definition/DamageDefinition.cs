using System;
using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    [Serializable]
    public record DamageDefinition : EffectDefinition
    {
        [SerializeField] private DamageType damageType;
        [SerializeField] private List<StatDependency> extraStatDeps;

        public DamagePackage CreatePackage(GameObject source, IStatGroup statComponent)
        {
            int finalAmount = amount;
            if (statComponent != null)
            {
                finalAmount = damageType.CreationScale(amount, statComponent, extraStatDeps);
            }

            return new DamagePackage(finalAmount, source, damageType);
        }
    }
}