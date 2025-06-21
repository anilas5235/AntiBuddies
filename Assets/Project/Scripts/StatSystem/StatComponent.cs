using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    /// <summary>
    /// Represents a component that manages all stats of an entity.
    /// Should only exist once per entity.
    /// </summary>
    [DefaultExecutionOrder(-50)]
    public class StatComponent : MonoBehaviour, IStatGroup
    {
        /// <summary>
        /// List of all stats managed by this component.
        /// Managed and represented in the editor.
        /// </summary>
        [SerializeField] private List<Stat> stats;

        /// <summary>
        /// Dictionary mapping StatType to IStat for quick access to stats managed by this component.
        /// initialized in Awake.
        /// </summary>
        private readonly Dictionary<StatType, IStat> _statDict = new();

        private void Awake()
        {
            InitStats();
            CallOnInitStats();
        }

        /// <summary>
        /// Initializes the stat dictionary from the stats list.
        /// </summary>
        private void InitStats()
        {
            foreach (Stat stat in stats)
            {
                _statDict.TryAdd(stat.StatType, stat);
            }
        }

        /// <summary>
        /// Calls OnStatInit on all child components that need stat initialization.
        /// </summary>
        private void CallOnInitStats()
        {
            INeedStatGroup[] comps = GetComponentsInChildren<INeedStatGroup>();
            foreach (INeedStatGroup component in comps)
            {
                component.OnStatInit(this);
            }
        }
        
        public IStat GetStat(StatType statType)
        {
            return _statDict.GetValueOrDefault(statType);
        }
       
        public void ModifyStat(StatPackage statPackage)
        {
            IStat stat = GetStat(statPackage.StatType);
            stat?.ModifyStat(statPackage);
        }

        /// <summary>
        /// Resets all stats to their initial state.
        /// </summary>
        public void ResetStats()
        {
            foreach (Stat liveStat in stats)
            {
                liveStat.Reset();
            }
        }
        
        /// <summary>
        /// Resets all temporary bonuses of the stats.
        /// </summary>
        public void ResetTempStats()
        {
            foreach (Stat liveStat in stats)
            {
                liveStat.ResetTempStat();
            }
        }

        private void OnValidate()
        {
            // Validate that all stats have a unique StatType and are initialized correctly.
            // This will only run in the editor.
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
    }
}