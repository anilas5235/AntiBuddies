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

        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (maxValue == value) return;
                maxValue = value;
                UpdateValues();
            }
        }

        public int MinValue
        {
            get => minValue;
            set
            {
                if (minValue == value) return;
                minValue = value;
                UpdateValues();
            }
        }

        public void UpdateValues()
        {
            statValue = baseStatValue + tempStatBonus;
            clampedValue = Mathf.Clamp(statValue, MinValue, MaxValue);
        }

        public int TransformPositive(int baseValue) => Transform(Value, baseValue);
        public int TransformNegative(int baseValue) => Transform(-Value, baseValue);

        private int Transform(int statVal, int baseValue) =>
            statType.IsPercentage ? Mathf.RoundToInt((1 + statVal / 100f) * baseValue) : baseValue + statVal;
        
        public void AddTempStatBonus(int value){
            tempStatBonus += value;
            UpdateValues();
        }
        
        public void AddPermanentStatBonus(int value){
            baseStatValue += value;
            UpdateValues();
        }
    }
}