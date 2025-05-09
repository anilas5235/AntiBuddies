using System;
using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    [DefaultExecutionOrder(-50)]
    public class StatComponent : MonoBehaviour, IStatGroup
    {
        [SerializeField] private StatDefaultData defaultStats;
        [SerializeField] private List<StatType> stats;
        [SerializeField] private List<Stat> liveStats;

        private readonly Dictionary<StatType, Stat> _statDict = new();

        private void Awake()
        {
            InitStats();
            CallOnInitStats();
        }

        private void OnValidate()
        {
            InitStats();
        }

        private void InitStats()
        {
            foreach (StatType statType in stats)
            {
                // Check if the statType is null
                if (!statType)
                {
                    Debug.LogWarning("StatType is null, skipping.");
                    continue;
                }

                // Check if the statType is already in the dictionary
                if (_statDict.ContainsKey(statType))
                {
                    Debug.LogWarning($"Stat {statType} already exists in the dictionary.");
                    continue;
                }

                Stat stat = new(statType, defaultStats.GetDefault(statType));
                liveStats.Add(stat);
                _statDict.Add(statType, stat);
            }
        }

        private void CallOnInitStats()
        {
            INeedStatComponent[] comps = GetComponentsInChildren<INeedStatComponent>();
            foreach (INeedStatComponent component in comps)
            {
                component.OnStatInit(this);
            }
        }

        public Stat GetStat(StatType statType)
        {
            return _statDict.GetValueOrDefault(statType);
        }

        public void ModifyStat(StatModification statModification)
        {
            Stat stat = GetStat(statModification.StatType);
            stat?.ModifyStat(statModification);
        }

        public void ModifyStat(EffectPackage<StatType> statPackage)
        {
            StatModification mod = new(statPackage.effectDef.EffectType, statPackage.effectDef.Amount, StatModification.Type.TempValue);
            ModifyStat(mod);
        }
    }
}