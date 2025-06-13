using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    [Serializable]
    public record HealDefinition : EffectDefinition
    {
        [SerializeField] private HealType healType;

        public HealPackage CreatePackage()
        {
            return new HealPackage(amount, healType);
        }
    }
}