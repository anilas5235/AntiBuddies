using System.Collections.Generic;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    /// <summary>
    /// Utility class for stat calculations and aggregations.
    /// </summary>
    public static class StatUtils
    {
        /// <summary>
        /// Applies a positive transformation (add or multiply) to a base value.
        /// </summary>
        /// <param name="baseValue">The value to transform.</param>
        /// <param name="value">The amount to add or multiply.</param>
        /// <param name="isPercentage">If true, applies as a percentage multiplier.</param>
        /// <returns>The transformed value.</returns>
        private static float TransformPositive(float baseValue, float value, bool isPercentage)
        {
            if (value == 0) return baseValue;
            if (!isPercentage) return baseValue + value;
            return baseValue * MakePositiveMultiplier(value);
        }

        /// <summary>
        /// Applies both percent and flat positive transformations.
        /// </summary>
        /// <param name="baseValue">The value to transform.</param>
        /// <param name="percent">The percent to apply.</param>
        /// <param name="flat">The flat value to add.</param>
        /// <returns>The transformed value.</returns>
        private static float TransformPositive(float baseValue, float percent, float flat)
        {
            // First apply percent, then flat.
            return TransformPositive(TransformPositive(baseValue, percent, true), flat, false);
        }

        /// <summary>
        /// Applies both flat and percent negative transformations.
        /// </summary>
        /// <param name="baseValue">The value to transform.</param>
        /// <param name="percent">The percent to apply.</param>
        /// <param name="flat">The flat value to subtract.</param>
        /// <returns>The transformed value.</returns>
        private static float TransformNegative(float baseValue, float percent, float flat)
        {
            // First apply flat, then percent.
            return TransformNegative(TransformNegative(baseValue, flat, false), percent, true);
        }

        /// <summary>
        /// Applies a negative transformation (subtract or divide) to a base value.
        /// </summary>
        /// <param name="baseValue">The value to transform.</param>
        /// <param name="value">The amount to subtract or divide.</param>
        /// <param name="isPercentage">If true, applies as a percentage divisor.</param>
        /// <returns>The transformed value.</returns>
        private static float TransformNegative(float baseValue, float value, bool isPercentage)
        {
            if (value == 0) return baseValue;
            if (!isPercentage) return baseValue - value;
            // Prevent division by zero or negative overflow
            if (value < -99) return 0;
            return baseValue / MakePositiveMultiplier(value);
        }

        /// <summary>
        /// Converts a percentage value to a multiplier (e.g., 20 becomes 1.2).
        /// </summary>
        /// <param name="percentage">The percentage value.</param>
        /// <returns>The multiplier.</returns>
        public static float MakePositiveMultiplier(float percentage)
        {
            if (percentage == 0) return 1f;
            return 1f + percentage / 100f;
        }

        /// <summary>
        /// Aggregates percent and flat values from a list of StatDependency.
        /// </summary>
        /// <param name="statDeps">The list of stat dependencies.</param>
        /// <param name="statGroup">The stat group to query values from.</param>
        /// <returns>A tuple containing the total percent and flat values.</returns>
        private static (float percent, float flat) AggregateStatDeps(List<StatDependency> statDeps,
            IStatGroup statGroup)
        {
            if (statDeps == null || statDeps.Count == 0) return (0, 0);
            List<StatRef> statRefs = new();
            foreach (StatDependency statDep in statDeps)
            {
                if (statDep == null || !statDep.IsValid) continue;
                StatRef statRef = new(statDep);
                statRef.OnStatInit(statGroup);
                statRefs.Add(statRef);
            }

            return AggregateStatRefs(statRefs);
        }

        /// <summary>
        /// Aggregates and applies stat dependencies to a base value.
        /// </summary>
        /// <param name="baseVal">The base value to transform.</param>
        /// <param name="statDeps">The list of stat dependencies.</param>
        /// <param name="statGroup">The stat group to query values from.</param>
        /// <param name="isPositive">If true (default), applies positive transformation; otherwise negative.</param>
        /// <returns>The transformed value.</returns>
        private static float AggregateStatDeps(float baseVal, List<StatDependency> statDeps, IStatGroup statGroup,
            bool isPositive = true)
        {
            (float percent, float flat) result = AggregateStatDeps(statDeps, statGroup);
            return isPositive
                ? TransformPositive(baseVal, result.percent, result.flat)
                : TransformNegative(baseVal, result.percent, result.flat);
        }

        /// <summary>
        /// Aggregates and applies stat dependencies to a base int value.
        /// </summary>
        /// <param name="baseVal">The base value to transform.</param>
        /// <param name="statDeps">The list of stat dependencies.</param>
        /// <param name="statGroup">The stat group to query values from.</param>
        /// <param name="isPositive">If true, applies positive transformation; otherwise negative.</param>
        /// <returns>The transformed integer value.</returns>
        public static int AggregateStatDeps(int baseVal, List<StatDependency> statDeps, IStatGroup statGroup,
            bool isPositive = true)
            => Mathf.RoundToInt(AggregateStatDeps((float)baseVal, statDeps, statGroup, isPositive));

        /// <summary>
        /// Aggregates percent and flat values from a list of StatRef.
        /// </summary>
        /// <param name="statRefs">The list of stat references.</param>
        /// <returns>A tuple containing the total percent and flat values.</returns>
        private static (float percent, float flat) AggregateStatRefs(List<StatRef> statRefs)
        {
            if (statRefs == null || statRefs.Count == 0) return (0, 0);
            float percent = 0;
            float flat = 0;
            foreach (StatRef statRef in statRefs)
            {
                if (statRef == null || !statRef.IsValid) continue;
                if (statRef.Stat.IsPercentage) percent += statRef.GetValue();
                else flat += statRef.GetValue();
            }

            return (percent, flat);
        }

        /// <summary>
        /// Aggregates and applies stat references to a base value.
        /// </summary>
        /// <param name="baseVal">The base value to transform.</param>
        /// <param name="statRefs">The list of stat references.</param>
        /// <param name="isPositive">If true, applies positive transformation; otherwise negative.</param>
        /// <returns>The transformed value.</returns>
        public static float AggregateStatRefs(float baseVal, List<StatRef> statRefs, bool isPositive = true)
        {
            (float percent, float flat) result = AggregateStatRefs(statRefs);
            return isPositive
                ? TransformPositive(baseVal, result.percent, result.flat)
                : TransformNegative(baseVal, result.percent, result.flat);
        }

        /// <summary>
        /// Aggregates and applies stat references to a base int value.
        /// </summary>
        /// <param name="baseVal">The base value to transform.</param>
        /// <param name="statRefs">The list of stat references.</param>
        /// <param name="isPositive">If true, applies positive transformation; otherwise negative.</param>
        /// <returns>The transformed integer value.</returns>
        public static int AggregateStatRefs(int baseVal, List<StatRef> statRefs, bool isPositive = true)
        {
            return Mathf.RoundToInt(AggregateStatRefs((float)baseVal, statRefs, isPositive));
        }
    }
}