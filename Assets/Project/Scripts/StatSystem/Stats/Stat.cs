using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class Stat : IStat
    {
        [SerializeField] private StatType statType;
        [SerializeField] private int statValue;
        [SerializeField] private int clampedValue;
        [SerializeField] private int maxValue = int.MaxValue;
        [SerializeField] private int minValue = int.MinValue;
        
        public StatType StatType => statType;

        public int Value
        {
            get => clampedValue;
            set
            {
                if (statValue == value) return;
                statValue = value;
                UpdateClampedValue();
            }
        }

        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (maxValue == value) return;
                maxValue = value;
                UpdateClampedValue();
            }
        }

        public int MinValue
        {
            get => minValue;
            set
            {
                if (minValue == value) return;
                minValue = value;
                UpdateClampedValue();
            }
        }
        public void UpdateClampedValue() => clampedValue = Mathf.Clamp(statValue, MinValue, MaxValue);
        public int TransformPositive(int baseValue) => Transform(Value, baseValue);
        public int TransformNegative(int baseValue) => Transform(-Value, baseValue);

        private int Transform(int statVal, int baseValue) =>
            statType.IsPercentage ? Mathf.RoundToInt((1 + statVal / 100f) * baseValue) : baseValue + statVal;
        public void MaximizeValue() => Value = MaxValue;

        public void MinimizeValue() => Value = MinValue;

        public bool RollChance()
        {
            if(statType.IsPercentage) return UnityEngine.Random.Range(0, 101) < Value;
            return false;
        }
    }
}