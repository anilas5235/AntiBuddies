using System;
using System.Collections.Generic;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    /// <summary>
    /// ScriptableObject representing a type of damage, including associated scaling and resistance stats.
    /// </summary>
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class DamageType : EffectType
    {
        /// <summary>
        /// The color associated with this damage type.
        /// </summary>
        [SerializeField] private Color color = Color.white;

        /// <summary>
        /// List of stat dependencies used for resistance calculations.
        /// </summary>
        [SerializeField] private List<StatDependency> resistanceStats;

        /// <summary>
        /// List of stat dependencies used for scaling damage.
        /// </summary>
        [SerializeField] private List<StatDependency> scaleStats;

        /// <summary>
        /// Gets the color associated with this damage type.
        /// </summary>
        public Color Color => color;

        /// <summary>
        /// Scales the damage amount based on stat dependencies and whether the scaling is positive or negative.
        /// </summary>
        /// <param name="amount">The base amount to scale.</param>
        /// <param name="statGroup">The stat group to use for scaling.</param>
        /// <param name="baseStats">The base stat dependencies.</param>
        /// <param name="extraStats">Additional stat dependencies.</param>
        /// <param name="isPositive">True for positive scaling, false for resistance.</param>
        /// <returns>The scaled amount, not less than zero.</returns>
        private static int ScaleAmount(int amount, IStatGroup statGroup, List<StatDependency> baseStats,
            List<StatDependency> extraStats, bool isPositive)
        {
            // Combine base and extra stat dependencies for scaling
            List<StatDependency> totalDeps = new(baseStats);
            if (extraStats is { Count: > 0 })
            {
                totalDeps.AddRange(extraStats);
            }

            int result = StatUtils.AggregateStatDeps(amount, totalDeps, statGroup, isPositive);
            return Math.Max(result, 0);
        }

        /// <summary>
        /// Scales the damage amount when the effect is created, using scaling stats.
        /// </summary>
        /// <param name="amount">The base damage amount.</param>
        /// <param name="statGroup">The stat group for scaling.</param>
        /// <param name="extraStats">Additional stat dependencies.</param>
        /// <returns>The scaled damage amount.</returns>
        public int CreationScale(int amount, IStatGroup statGroup, List<StatDependency> extraStats)
        {
            return ScaleAmount(amount, statGroup, scaleStats, extraStats, true);
        }

        /// <summary>
        /// Scales the damage amount when the effect is received, using resistance stats.
        /// </summary>
        /// <param name="amount">The base damage amount.</param>
        /// <param name="statGroup">The stat group for resistance.</param>
        /// <returns>The scaled damage amount after resistance.</returns>
        public int ReceptionScale(int amount, IStatGroup statGroup)
        {
            return ScaleAmount(amount, statGroup, resistanceStats, null, false);
        }
    }
}