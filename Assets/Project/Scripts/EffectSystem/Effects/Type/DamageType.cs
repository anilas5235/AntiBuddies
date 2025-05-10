using System.Collections.Generic;
using System.Linq;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class DamageType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private List<StatType> flatResistanceStat;
        [SerializeField] private List<StatType> percentResistanceStat;
        [SerializeField] private List<StatType> flatScaleStat;
        [SerializeField] private List<StatType> percentScaleStat;

        public Color Color => color;
        public override int CreationScale(int amount, StatComponent statComponent)
        {
            amount = AggregateStats(amount, statComponent, percentScaleStat, true);
            amount = AggregateStats(amount, statComponent, flatScaleStat, true);
            return amount;
        }

        public override int ReceptionScale(int amount, StatComponent statComponent)
        {
            amount = AggregateStats(amount, statComponent, flatResistanceStat, false);
            amount = AggregateStats(amount, statComponent, percentResistanceStat, false);
            return amount;
        }

        private static int AggregateStats(int value, StatComponent statComponent, List<StatType> stats, bool isPositive)
        {
            if (stats == null || !statComponent) return value;

            return stats.Aggregate(value, (current, stat) =>
                isPositive
                    ? statComponent.GetStat(stat)?.TransformPositive(current) ?? current
                    : statComponent.GetStat(stat)?.TransformNegative(current) ?? current);
        }
    }
}
