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
        [SerializeField] private List<Stat> stats;

        private readonly Dictionary<StatType, IStat> _statDict = new();

        private void Awake()
        {
            InitStats();
            CallOnInitStats();
        }

        private void InitStats()
        {
            foreach (Stat stat in stats)
            {
                _statDict.TryAdd(stat.StatType, stat);
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

        public IStat GetStat(StatType statType)
        {
            return _statDict.GetValueOrDefault(statType);
        }

        public void ModifyStat(EffectPackage<StatType> statPackage)
        {
            IStat stat = GetStat(statPackage.EffectType);
            stat?.ModifyStat(statPackage);
        }

        public void ResetStats()
        {
            foreach (Stat liveStat in stats)
            {
                liveStat.Reset();
            }
        }

        private void OnValidate()
        {
            List<StatType> statTypes = new();
            foreach (Stat stat in stats)
            {
                if (!stat.StatType)
                {
                    Debug.LogWarning($"Stat {stat} has no StatType assigned. Please assign a valid StatType.");
                    continue;
                }

                if (statTypes.Contains(stat.StatType))
                {
                    Debug.LogWarning(
                        $"Duplicate StatType {stat.StatType} found in {name}. Please ensure each StatType is unique.");
                    continue;
                }

                statTypes.Add(stat.StatType);

                stat.UpdateValues();
            }
        }

        public void ResetStatOfType(StatType statType)
        {
            IEnumerable<Stat> s = stats.Where(x => x.StatType == statType);
            foreach (Stat stat in s)
            {
                stat.Reset();
            }
        }
    }
}