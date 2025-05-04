using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class Stat : IStat
    {
        [SerializeField] private StatType statType;
        [SerializeField] private int statValue;
        [SerializeField] private int clampedValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        public Stat(StatType statType, int statValue = 0)
        {
            this.statType = statType;
            this.statValue = statValue;
            maxValue = statType.MaxValue;
            minValue = statType.MinValue;
            UpdateClampedValue();
        }

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
    }
}
