using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.StatSystem
{
    public static class StatUtils
    {
        private static float TransformPositive(float baseValue, float value, bool isPercentage)
        {
            if (value == 0) return baseValue;
            if (!isPercentage) return baseValue + value;
            return baseValue * MakePositiveMultiplier(value);
        }

        private static float TransformPositive(float baseValue, float percent, float flat)
        {
            return TransformPositive(TransformPositive(baseValue, percent, true), flat, false);
        }

        private static float TransformNegative(float baseValue, float percent, float flat)
        {
            return TransformNegative(TransformNegative(baseValue, flat, false), percent, true);
        }

        private static float TransformNegative(float baseValue, float value, bool isPercentage)
        {
            if (value == 0) return baseValue;
            if (!isPercentage) return baseValue - value;
            if (value < -99) return 0;
            return baseValue / MakePositiveMultiplier(value);
        }

        private static float MakePositiveMultiplier(float percentage)
        {
            if (percentage == 0) return 1f;
            return 1f + percentage / 100f;
        }

        private static (float percent, float flat) AggregateStatDeps(List<StatDependency> statDeps, IStatGroup statGroup)
        {
            float percent = 0;
            float flat = 0;
            foreach (StatDependency statDep in statDeps)
            {
                if (statDep == null ||!statDep.StatType) continue;
                if (statDep.IsPercentage) percent += statDep.GetValue(statGroup);
                else flat += statDep.GetValue(statGroup);
            }
            return (percent, flat);
        }

        private static float AggregateStatDeps(float baseVal, List<StatDependency> statDeps, IStatGroup statGroup,
            bool isPositive = true)
        {
            (float percent, float flat) result = AggregateStatDeps(statDeps, statGroup);
            return isPositive
                ? TransformPositive(baseVal, result.percent, result.flat)
                : TransformNegative(baseVal, result.percent, result.flat);
        }

        public static int AggregateStatDeps(int baseVal, List<StatDependency> statDeps, IStatGroup statGroup,
            bool isPositive = true)
            => Mathf.RoundToInt(AggregateStatDeps((float)baseVal, statDeps, statGroup, isPositive));

        private static (float percent, float flat) AggregateStatRefs(List<StatRef> statRefs)
        {
            float percent = 0;
            float flat = 0;
            foreach (StatRef statRef in statRefs)
            {
                if (statRef?.Stat == null) continue;
                if (statRef.Stat.IsPercentage) percent += statRef.GetValue();
                else flat += statRef.GetValue();
            }
            return (percent, flat);
        }
        
        public static float AggregateStatRefs(float baseVal, List<StatRef> statRefs, bool isPositive = true)
        {
            (float percent, float flat) result = AggregateStatRefs(statRefs);
            return isPositive
                ? TransformPositive(baseVal, result.percent, result.flat)
                : TransformNegative(baseVal, result.percent, result.flat);
        }

        public static int AggregateStatRefs(int baseVal, List<StatRef> statRefs, bool isPositive = true)
        {
            return Mathf.RoundToInt(AggregateStatRefs((float)baseVal, statRefs, isPositive));
        }
    }
}