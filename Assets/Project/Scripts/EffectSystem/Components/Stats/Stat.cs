using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.EffectSystem.Components.Stats
{
    [Serializable]
    public class Stat : IStat
    {
        [SerializeField] private StatType statType;
        [SerializeField] private int statValue;
        [SerializeField] private bool isPercentage;
        [SerializeField] private bool isClamped;

        public event Action<int> OnChange;

        [SerializeField] private int clampedValue;
        [SerializeField] private int maxValue = 100;
        [SerializeField] private int minValue;

        public Stat()
        {
        }

        public Stat(int initialVal, int max, int min)
        {
            statValue = initialVal;
            maxValue = max;
            minValue = min;
            isClamped = true;
            UpdateClampedValue();
        }

        protected int StatValue
        {
            get => isClamped ? clampedValue : statValue;
            set
            {
                if (statValue == value) return;
                statValue = value;
                OnStatValueChanged();
                OnChange?.Invoke(statValue);
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

        public StatType StatType => statType;

        public bool IsBelowOrZero()
        {
            return StatValue <= 0;
        }

        public virtual void ReduceValue(int amount)
        {
            StatValue -= amount;
        }

        public virtual void IncreaseValue(int amount)
        {
            StatValue += amount;
        }

        public int TransformPositive(int baseValue) => Transform(StatValue, baseValue);
        public int TransformNegative(int baseValue) => Transform(-StatValue, baseValue);

        private int Transform(int statVal, int baseValue)
        {
            if (isPercentage)
            {
                return Mathf.RoundToInt((1 + statVal / 100f) * baseValue);
            }

            return baseValue + statVal;
        }

        protected void OnStatValueChanged()
        {
            UpdateClampedValue();
        }

        public void UpdateClampedValue()
        {
            clampedValue = Mathf.Clamp(StatValue, MinValue, MaxValue);
        }

        public void MaximizeValue()
        {
            if (isClamped) StatValue = MaxValue;
        }
    }
}