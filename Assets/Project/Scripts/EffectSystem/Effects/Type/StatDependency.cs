using System;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [Serializable]
    public class StatDependency
    {
        [FormerlySerializedAs("usePercent")] [SerializeField,Range(0,500)] private int useEfficiency = 100;
        [SerializeField] private StatType statType;
        public bool IsPercentage => statType.IsPercentage;
        public StatType StatType => statType;
        
        public StatDependency()
        {
        }

        public StatDependency(StatType statType, int useEfficiency = 100)
        {
            this.statType = statType;
            this.useEfficiency = useEfficiency;
        }
        
        public float GetValue(IStat stat)
        {
            if(stat == null) return 0f;
            return stat.Value * useEfficiency / 100f;
        }
        
        public float GetValue(IStatGroup statGroup)
        {
            return GetValue(statGroup.GetStat(statType));
        }
    }
}