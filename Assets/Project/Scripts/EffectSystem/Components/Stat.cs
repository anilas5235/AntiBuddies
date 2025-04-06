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

        public Stat()
        {
            currValue = 0;
            maxValue = 100;
            minValue = 0;
        }

        public Stat(int currValue, int maxValue, int minValue = 0)
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

        public virtual int GetTransformedValue(int baseValue)
        {
            return baseValue + currValue;
        }

        public bool IsBelowOrZero() => currValue <= 0;

        public void ReduceValue(int amount) => currValue = Mathf.Clamp(currValue - amount, minValue, maxValue);

        public void IncreaseValue(int amount) => currValue = Mathf.Clamp(currValue - amount, minValue, maxValue);
        
        public void MaximizeValue() => currValue = maxValue;
    }
}