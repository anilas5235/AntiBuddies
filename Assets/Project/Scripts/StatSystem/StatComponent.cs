using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    public class StatComponent : MonoBehaviour
    {
        [SerializeField] private List<StatType> stats;
        [SerializeField] private List<Stat> liveStats;

        private Dictionary<StatType, Stat> _statDict;

        private void Awake()
        {
            _statDict = new Dictionary<StatType, Stat>();
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

                Stat stat = new(statType);
                liveStats.Add(stat);
                _statDict.Add(statType, stat);
            }
        }

        public Stat GetStat(StatType statType)
        {
            return _statDict.GetValueOrDefault(statType);
        }
    }
}