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

        [SerializeField] private int percentMultiplier;

        [SerializeField] private int maxValue;
        [SerializeField] private int minValue;

        [SerializeField] private int baseStatValue;
        [SerializeField] private int tempStatBonus;

        public StatType StatType => statType;

        public Stat(StatType statType, int statValue = 0)
        {
            this.statType = statType;
            baseStatValue = statValue;
            maxValue = statType.MaxValue;
            minValue = statType.MinValue;
            UpdateValues();
        }

        public event Action OnStatChange;
        public int Value => clampedValue;
        public int FreeValue => statValue;
        public int MaxValue => maxValue;
        public int MinValue => minValue;

        public void UpdateValues()
        {
            statValue = Mathf.RoundToInt((baseStatValue + tempStatBonus) * MakePositiveMultiplier(percentMultiplier));
            clampedValue = Mathf.Clamp(statValue, MinValue, MaxValue);
            OnStatChange?.Invoke();
        }

        public float TransformPositive(float baseValue)
        {
            if (Value == 0) return baseValue;
            if (!statType.IsPercentage) return baseValue + Value;
            return baseValue * MakePositiveMultiplier(Value);
        }

        public float TransformNegative(float baseValue)
        {
            if (Value == 0) return baseValue;
            if (!statType.IsPercentage) return baseValue - Value;
            if(Value < -99) return 0;
            return baseValue / MakePositiveMultiplier(Value);
        }

        private float MakePositiveMultiplier(float multiplier)
        {
            if (multiplier == 0) return 1f;
            return 1f + multiplier / 100f;
        }

        public void ModifyStat(StatModification statModification)
        {
            if (statModification.StatType != statType) return;

            switch (statModification.ModType)
            {
                case StatModification.Type.BaseValue:
                    baseStatValue += statModification.Value;
                    break;
                case StatModification.Type.TempValue:
                    tempStatBonus += statModification.Value;
                    break;
                case StatModification.Type.MaxValue:
                    maxValue = statModification.Value;
                    break;
                case StatModification.Type.MinValue:
                    minValue = statModification.Value;
                    break;
            }

            UpdateValues();
        }

        public void Reset()
        {
            baseStatValue = 0;
            tempStatBonus = 0;
            percentMultiplier = 0;
            maxValue = statType.MaxValue;
            minValue = statType.MinValue;
            UpdateValues();
        }
    }
}