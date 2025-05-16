using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    public interface IStat
    {
        public event Action OnStatChange;
        public int Value { get; }
        public bool IsPercentage { get; }
        public int TransformPositive(int baseValue) => Mathf.RoundToInt(TransformPositive((float)baseValue));
        public int TransformNegative(int baseValue) => Mathf.RoundToInt(TransformNegative((float)baseValue));
        public float TransformPositive(float baseValue);
        public float TransformNegative(float baseValue);
        public void ModifyStat(StatModification statModification);

        public void ModifyStat(EffectPackage<StatType> statPackage)
        {
            StatModification mod = new(statPackage.effectDef.EffectType, statPackage.effectDef.Amount,
                StatModification.Type.TempValue);
            ModifyStat(mod);
        }
    }
}