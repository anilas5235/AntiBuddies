using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    [CreateAssetMenu(fileName = "StatDefaultData", menuName = "StatSystem/StatDefaultData")]
    public class StatDefaultData : ScriptableObject
    {
        [SerializeField] private List<DefaultStat> defaults;
        public int GetDefault(StatType statType)
        {
            DefaultStat def = defaults.FirstOrDefault(defaultStat => defaultStat.statType == statType);
            return def?.value ?? 0;
        }

        [Serializable]
        public class DefaultStat
        {
            public StatType statType;
            public int value;
        }
    }
}