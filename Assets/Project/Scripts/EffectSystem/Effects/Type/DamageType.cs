using System;
using System.Collections.Generic;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class DamageType : EffectType
    {
        [SerializeField] private Color color = Color.white;

        [SerializeField] private List<StatDependency> resistanceStats;

        [SerializeField] private List<StatDependency> scaleStats;

        public Color Color => color;

        private static int ScaleAmount(int amount, IStatGroup statGroup, List<StatDependency> baseStats,
            List<StatDependency> extraStats, bool isPositive)
        {
            List<StatDependency> totalDeps = new(baseStats);
            if (extraStats is { Count: > 0 })
            {
                totalDeps.AddRange(extraStats);
            }

            int result = StatUtils.AggregateStatDeps(amount, totalDeps, statGroup, isPositive);
            return Math.Max(result, 0);
        }

        public int CreationScale(int amount, IStatGroup statGroup, List<StatDependency> extraStats)
        {
            return ScaleAmount(amount, statGroup, scaleStats, extraStats, true);
        }

        public int ReceptionScale(int amount, IStatGroup statGroup)
        {
            return ScaleAmount(amount, statGroup, resistanceStats, null, false);
        }
    }
}