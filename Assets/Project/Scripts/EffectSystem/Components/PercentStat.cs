using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    [Serializable]
    public class PercentStat : Stat
    {
        public PercentStat() : base(0,100,0)
        {
        }
        
        public PercentStat(int currValue, int maxValue, int minValue) : base(currValue,maxValue,minValue)
        {
        }

        public override int TransformPositive(int baseValue)
        {
            return Mathf.RoundToInt((1+CurrValue/100f) * baseValue);
        }
        
        public override int TransformNegative(int baseValue)
        {
            return Mathf.RoundToInt((1-CurrValue/100f) * baseValue);
        }
    }
}