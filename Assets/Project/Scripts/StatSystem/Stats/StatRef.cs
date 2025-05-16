using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class StatRef
    {
        [SerializeField] private StatType statType;
        public StatType StatType => statType;
        public IStat Stat { get; private set; }

        public virtual void Init(StatComponent statComponent)
        {
            if (!statComponent) return;
            if(!statType) throw new ArgumentNullException(nameof(statType), "cannot be null.");
            Stat = statComponent.GetStat(statType);
            if (Stat == null)
            {
                Debug.LogWarning($"Stat {statType} not found in StatComponent.");
            }
        }
    }
}