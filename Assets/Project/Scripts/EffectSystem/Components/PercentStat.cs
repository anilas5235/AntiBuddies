using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    [Serializable]
    public class PercentStat : Stat
    {
        public PercentStat()
        {
        }
        
        public PercentStat(int currValue, int maxValue, int minValue = 0) : base(currValue,maxValue,minValue)
        {
        }
        
        public override int GetTransformedValue(int baseValue)
        {
            return Mathf.RoundToInt((1-CurrValue/100f) * baseValue);
        }
    }
}