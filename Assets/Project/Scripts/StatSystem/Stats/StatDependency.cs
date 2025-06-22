using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// Represents a dependency on a stat, allowing retrieval of its value based on a specified efficiency.
    /// The efficiency is a percentage that determines how much of the stat's value is used.
    /// </summary>
    [Serializable]
    public class StatDependency
    {
        /// <summary>
        /// The efficiency percentage used to scale the stat value.
        /// </summary>
        [SerializeField,Range(0,500)] private int useEfficiency;
        
        /// <summary>
        /// The type of the stat this dependency refers to.
        /// </summary>
        [SerializeField] private StatType statType;
        
        /// <summary>
        /// The type of the stat this dependency refers to.
        /// </summary>
        public StatType StatType => statType;
        
        /// <summary>
        /// Returns true if the stat type is assigned.
        /// </summary>
        public bool IsValid => statType;
        
        public StatDependency()
        {
            useEfficiency = 100;
        }
        
        /// <param name="statType">The stat type this dependency refers to.</param>
        /// <param name="useEfficiency">The efficiency percentage (default 100).</param>
        public StatDependency(StatType statType, int useEfficiency = 100)
        {
            this.statType = statType;
            this.useEfficiency = useEfficiency;
        }
        
        /// <summary>
        /// Gets the value from a stat, scaled by the efficiency percentage.
        /// </summary>
        /// <param name="stat">The stat to retrieve the value from.</param>
        /// <returns>The scaled stat value.</returns>
        public float GetValue(IStat stat)
        {
            if(stat == null) return 0f;
            // Multiply stat value by efficiency percentage.
            return useEfficiency == 100 ? stat.Value :  stat.Value * useEfficiency / 100f;
        }
        
        /// <summary>
        /// Gets the value from a stat group, scaled by the efficiency percentage.
        /// </summary>
        /// <param name="statGroup">The stat group to retrieve the stat from.</param>
        /// <returns>The scaled stat value.</returns>
        public float GetValue(IStatGroup statGroup)
        {
            return GetValue(statGroup.GetStat(statType));
        }
    }
}