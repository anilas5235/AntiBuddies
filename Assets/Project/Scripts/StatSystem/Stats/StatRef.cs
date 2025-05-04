using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class StatRef
    {
        [SerializeField] private StatType statType;
        private Stat _stat;
        public StatType StatType => statType;
        public Stat Stat => _stat;
        
        public void GetStat(StatComponent statComponent)
        {
            if (!statComponent) return;
            _stat = statComponent.GetStat(statType);
        }
    }
}