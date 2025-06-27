using System;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// Represents a reference to a stat, allowing retrieval of its value based on a dependency.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public class StatRef : INeedStatGroup
    {
        [SerializeField] private StatDependency statDependency = new();

        public StatRef()
        {
        }

        public StatRef(StatDependency statDependency)
        {
            this.statDependency = statDependency ?? new StatDependency();
        }

        /// <summary>
        /// The referenced stat instance.
        /// </summary>
        public IStat Stat { get; private set; }

        /// <summary>
        /// Returns true if the stat dependency is valid and the stat is assigned.
        /// </summary>
        public bool IsValid => statDependency.IsValid && Stat != null;

        /// <inheritdoc/>
        public void OnStatInit(IStatGroup statGroup)
        {
            if (statGroup == null || !statDependency.IsValid) return;
            Stat = statGroup.GetStat(statDependency.StatType);
        }

        /// <summary>
        /// Gets the value from the referenced stat using the dependency.
        /// </summary>
        /// <returns>The value of the stat.</returns>
        public float GetValue()
        {
            return IsValid ? statDependency.GetValue(Stat) : 0f;
        }
        
        public float GetValueAsPercentage()
        {
            return IsValid ? statDependency.GetValue(Stat) / 100f : 0f;
        }
    }
}