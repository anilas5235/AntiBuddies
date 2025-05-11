using System.Collections.Generic;
using System.Linq;
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
            CallOnInitStats();
        }

        public void CheckLiveStats()
        {
            foreach (StatType statType in stats)
            {
                if (liveStats.FirstOrDefault(s => s.StatType == statType) != null) continue;

                Stat stat = new(statType, defaultStats.GetDefault(statType));
                liveStats.Add(stat);
            }

            while (liveStats.Count > stats.Count)
            {
                liveStats.RemoveAt(liveStats.Count - 1);
            }
        }

        private void InitStats()
        {
            CheckLiveStats();

            foreach (Stat stat in liveStats)
            {
                _statDict.TryAdd(stat.StatType, stat);
            }
        }

        private void CallOnInitStats()
        {
            InitStats();
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
            StatModification mod = new(statPackage.effectDef.EffectType, statPackage.effectDef.Amount,
                StatModification.Type.TempValue);
            ModifyStat(mod);
        }

        public void ResetLiveStats()
        {
            foreach (Stat liveStat in liveStats)
            {
                liveStat.Reset();
            }
        }

        public void ForceStatsToUpdate()
        {
            foreach (Stat liveStat in liveStats)
            {
                liveStat.UpdateValues();
            }
        }
    }
}