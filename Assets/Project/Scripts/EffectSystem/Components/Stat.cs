using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private int currValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        public Stat(): this(0, 100, -100)
        {
        }
        
        public Stat(int currValue, int maxValue, int minValue)
        {
            this.currValue = currValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }
        
        protected int CurrValue => currValue;

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                currValue = Mathf.Clamp(currValue, minValue, maxValue);
            }
        }

        public int MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                currValue = Mathf.Clamp(currValue, minValue, maxValue);
            }
        }

        public virtual int TransformPositive(int baseValue) => baseValue + currValue;

        public virtual int TransformNegative(int baseValue) => baseValue - currValue;

        public bool IsBelowOrZero() => currValue <= 0;

        public void ReduceValue(int amount) => currValue = Mathf.Clamp(currValue - amount, minValue, maxValue);

        public void IncreaseValue(int amount) => currValue = Mathf.Clamp(currValue - amount, minValue, maxValue);
        
        public void MaximizeValue() => currValue = maxValue;
    }
}