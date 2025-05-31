using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class StatRef
    {
        [SerializeField] private StatDependency statDependency = new();
        public IStat Stat { get; private set; }
        
        public bool IsValid => statDependency.IsValid || Stat != null;

        public void Init(IStatGroup statComponent)
        {
            if (statComponent == null) return;
            StatType statType = statDependency.StatType;
            if (!statType) throw new ArgumentNullException(nameof(statType), "cannot be null.");
            Stat = statComponent.GetStat(statType);
        }

        public float GetValue()
        {
            return statDependency.GetValue(Stat);
        }
    }
}