using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    public interface IStat
    {
        public event Action OnStatChange;
        public int Value { get; }
        public bool IsPercentage { get; }
        public StatType StatType { get; }
       
        public void ModifyStat(StatPackage package);
        
    }
}