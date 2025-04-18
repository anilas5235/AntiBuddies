using System;
using Project.Scripts.EffectSystem.Components.Stats.StatBehaviour;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components.Stats
{
    [Serializable]
    public class ClampedStat : BaseStat
    {
        [SerializeField] private int clampedValue;
        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        protected int ClampedValue => clampedValue;

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

        internal ClampedStat(IStatBehaviour statBehaviour, int value, int max, int min) : base(statBehaviour, value)
        {
            clampedValue = value;
            maxValue = max;
            minValue = min;
            UpdateClampedValue();
        }

        public ClampedStat() : this(0, 100, 0)
        {
        }

        public ClampedStat(int value, int max, int min) : this(new FlatStatBehaviour(), value, max, min)
        {
        }

        protected override void OnStatValueChanged()
        {
            UpdateClampedValue();
        }

        private void UpdateClampedValue()
        {
            clampedValue = Mathf.Clamp(StatValue, MinValue, MaxValue);
        }

        public void MaximizeValue() => StatValue = MaxValue;
    }
}