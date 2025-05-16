using System.Collections.Generic;
using System.Linq;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class DamageType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private List<StatType> ResistanceStats;
        [SerializeField] private List<StatType> ScaleStats;
        public Color Color => color;

        public override int CreationScale(int amount, StatComponent statComponent, List<IStat> extraStats = null)
        {
            return CalculateScaledAmount(amount, statComponent, ScaleStats, extraStats, true);
        }

        public override int ReceptionScale(int amount, StatComponent statComponent, List<IStat> extraStats = null)
        {
            return CalculateScaledAmount(amount, statComponent, ResistanceStats, extraStats, false);
        }

        private static int CalculateScaledAmount(int amount, StatComponent statComponent, List<StatType> statTypes, List<IStat> extraStats, bool isPositive)
        {
            List<IStat> stats = statTypes.Select(statComponent.GetStat).Where(statValue => statValue != null).ToList();

            if (extraStats != null) stats.AddRange(extraStats);
            int currAmount = amount;
            int flatAmount = 0;

            foreach (IStat stat in stats.Where(stat => stat != null))
            {
                if (stat.IsPercentage)
                    currAmount = isPositive ? stat.TransformPositive(currAmount) : stat.TransformNegative(currAmount);
                else
                    flatAmount += isPositive ? stat.TransformPositive(currAmount) : stat.TransformNegative(currAmount);
            }

            int finalAmount = currAmount + flatAmount;
            return finalAmount < 1 ? 0 : finalAmount;
        }
    }
}
