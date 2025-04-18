using System;
using Project.Scripts.EffectSystem.Components.Stats.StatBehaviour;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components.Stats
{
    [Serializable]
    public class ClampedPercentStat : ClampedStat
    {
        internal ClampedPercentStat(int currValue, int maxValue , int minValue) : base(new PercentStatBehaviour(), currValue, maxValue, minValue)
        {
        }
        
        public ClampedPercentStat() : base(0,100,0)
        {
        }
    }
}