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
        
        public int Value
        {
            get => currValue;
            set => currValue = Mathf.Clamp(value, minValue, maxValue);
        }
        
        public float Percent => currValue / 100f;

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
    }
}