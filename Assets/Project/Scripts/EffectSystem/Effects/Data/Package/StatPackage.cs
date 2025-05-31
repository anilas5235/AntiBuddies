using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    [Serializable]
    public class StatPackage : EffectPackage
    {
        [SerializeField] private StatType statType;
        [SerializeField] private StatModification statMod;

        public StatPackage(int amount, StatType statType, StatModification statMod) : base(amount)
        {
            this.statType = statType;
            this.statMod = statMod;
        }

        public StatType StatType => statType;
        public StatModification StatMod => statMod;

        public enum StatModification
        {
            BaseValue,
            TempValue,
            MaxValue,
            MinValue
        }

        public StatPackage Inverse() => new(-Amount, StatType, statMod);
    }
}