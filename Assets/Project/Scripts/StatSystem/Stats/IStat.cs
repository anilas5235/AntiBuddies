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
        public StatType StatType { get; }
       
        public void ModifyStat(StatModification statModification);

        public void ModifyStat(EffectPackage<StatType> statPackage)
        {
            StatModification mod = new(statPackage.effectDef.EffectType, statPackage.effectDef.Amount,
                StatModification.Type.TempValue);
            ModifyStat(mod);
        }
    }
}