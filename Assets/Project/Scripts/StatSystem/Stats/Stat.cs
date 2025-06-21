using System;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// Represents a single stat in the stat system.
    /// </summary>
    [Serializable]
    public class Stat : IStat
    {
        /// <summary>
        /// The type of the stat, defining its properties and behavior.
        /// </summary>
        [SerializeField] private StatType statType;
        
        /// <summary>
        /// The current value of the stat, which include bonuses and multipliers.
        /// </summary>
        [SerializeField] private int statValue;
        
        /// <summary>
        /// The clamped value of the stat, showing the final value after applying min and max limits.
        /// </summary>
        [SerializeField] private int clampedValue;

        /// <summary>
        /// The percentage multiplier applied to the stat value as the last step in the calculation.
        /// </summary>
        [SerializeField] private int percentMultiplier;

        /// <summary>
        /// The maximum value the stat should have.
        /// </summary>
        [SerializeField] private int maxValue;
        
        /// <summary>
        /// The minimum value the stat should have.
        /// </summary>
        [SerializeField] private int minValue;

        /// <summary>
        /// The base value of the stat, which is the initial value before any bonuses or multipliers are applied.
        /// </summary>
        [SerializeField] private int baseStatValue;
        
        /// <summary>
        /// Temporary bonus to the stat value, which can be modified by effects or other game mechanics.
        /// </summary>
        [SerializeField] private int tempStatBonus;
        
        // Track the previous value of statType, only relevant in the editor
        private StatType _previousStatType; 

        /// <param name="statType">The type of the stat.</param>
        /// <param name="statValue">Initial value for the stat.</param>
        public Stat(StatType statType, int statValue = 0)
        {
            this.statType = statType;
            baseStatValue = statValue;
            maxValue = statType.MaxValue;
            minValue = statType.MinValue;
            UpdateValues();
        }

        /// <inheritdoc/>
        public StatType StatType => statType;
        /// <inheritdoc/>
        public event Action OnStatChange;
        /// <inheritdoc/>
        public int Value => clampedValue;
        /// <inheritdoc/>
        public bool IsPercentage => statType.IsPercentage;
        /// <inheritdoc/>
        public int FreeValue => statValue;
        /// <inheritdoc/>
        public int MaxValue => maxValue;
        /// <inheritdoc/>
        public int MinValue => minValue;

        /// <inheritdoc/>
        public void UpdateValues()
        {
            // Calculate the stat value with bonuses and percent multipliers.
            statValue = Mathf.RoundToInt((baseStatValue + tempStatBonus) * StatUtils.MakePositiveMultiplier(percentMultiplier));
            clampedValue = Mathf.Clamp(statValue, MinValue, MaxValue);
            OnStatChange?.Invoke();
        }

        /// <inheritdoc/>
        public void ModifyStat(StatPackage package)
        {
            if (package.StatType != statType) return;

            switch (package.StatMod)
            {
                case StatPackage.StatModification.BaseValue:
                    baseStatValue += package.Amount;
                    break;
                case StatPackage.StatModification.TempValue:
                    tempStatBonus += package.Amount;
                    break;
                case StatPackage.StatModification.MaxValue:
                    maxValue = package.Amount;
                    break;
                case StatPackage.StatModification.MinValue:
                    minValue = package.Amount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            UpdateValues();
        }

        /// <inheritdoc/>
        public void Reset()
        {
            Debug.Log("Resetting stat: " + statType);
            baseStatValue = 0;
            tempStatBonus = 0;
            percentMultiplier = 0;
            maxValue = statType ? statType.MaxValue : 0;
            minValue = statType ? statType.MinValue : 0;
            UpdateValues();
        }

        /// <inheritdoc/>
        public void ResetTempStat()
        {
            tempStatBonus = 0;
            UpdateValues();
        }
    }
}