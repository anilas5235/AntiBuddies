using System;
using System.Collections.Generic;
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

        [SerializeField] private int baseStatValue;
        [SerializeField] private int tempStatBonus;

        public Stat(StatType statType, int statValue = 0)
        {
            this.statType = statType;
            baseStatValue = statValue;
            maxValue = statType.MaxValue;
            minValue = statType.MinValue;
            UpdateValues();
        }

        public float AsFloatPercentage => 1 + Value / 100f;
        public int Value => clampedValue;
        public int FreeValue => statValue;
        public int MaxValue => maxValue;
        public int MinValue => minValue;
        public event Action OnStatChange;

        private void UpdateValues()
        {
            statValue = baseStatValue + tempStatBonus;
            clampedValue = Mathf.Clamp(statValue, MinValue, MaxValue);
            OnStatChange?.Invoke();
        }

        public int TransformPositive(int baseValue) => Transform(Value, baseValue);
        public int TransformNegative(int baseValue) => Transform(-Value, baseValue);

        private int Transform(int statVal, int baseValue) =>
            statType.IsPercentage ? Mathf.RoundToInt((1 + statVal / 100f) * baseValue) : baseValue + statVal;

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
    }
}