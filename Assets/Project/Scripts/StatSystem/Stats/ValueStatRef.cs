using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// Represents a value that is calculated based on multiple stat references.
    /// It aggregates the values of its stat references to produce a final value.
    /// </summary>
    [Serializable]
    public class ValueStatRef : INeedStatGroup
    {
        [SerializeField] private List<StatRef> statRefs;
        [SerializeField] private float baseValue = 1f;
        [SerializeField] private float currValue;
        [SerializeField] private bool positiveTransform = true;
        
        private IStatGroup _statGroup;

        /// <summary>
        /// The current calculated value after applying all stat references.
        /// </summary>
        public float CurrValue => currValue;

        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event Action OnValueChange;

        /// <summary>
        /// The base value before stat modifications.
        /// </summary>
        public float BaseValue
        {
            get => baseValue;
            set => baseValue = value;
        }

        /// <summary>
        /// Updates the current value by aggregating all stat references.
        /// </summary>
        internal void UpdateValue()
        {
            currValue = StatUtils.AggregateStatRefs(baseValue, statRefs, positiveTransform);
            OnValueChange?.Invoke();
        }

        /// <summary>
        /// Adds a new stat reference dependency to this value stat reference.
        /// </summary>
        /// <param name="statDependency">The dependency defining which stat to reference.</param>
        public void AddStat(StatDependency statDependency)
        {
            StatRef statRef = new(statDependency);

            statRef.OnStatInit(_statGroup);
            statRefs.Add(statRef);
            statRef.Stat.OnStatChange += UpdateValue;
            UpdateValue();
        }
       
        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
            foreach (StatRef statRef in statRefs)
            {
                statRef.OnStatInit(_statGroup);
                statRef.Stat.OnStatChange += UpdateValue;
            }

            UpdateValue();
        }
    }
}