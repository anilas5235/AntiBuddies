using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    [Serializable]
    public class HealPackage : EffectPackage
    {
        [SerializeField] private HealType healType;

        public HealPackage(int amount, HealType healType) : base(amount)
        {
            this.healType = healType;
        }
        
        public HealType HealType => healType;
    }
}