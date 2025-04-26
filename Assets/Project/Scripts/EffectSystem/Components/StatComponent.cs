using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class StatComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private StatType healthStatType;
        [SerializeField] private List<Stat> stats = new();

        public int ResistAttack(int damage, AttackType attackType)
        {
            if (attackType.AffectedByFlatModifier)
            {
                Stat flatModifier = GetStat(attackType.FlatModifier);
                if (flatModifier != null)
                {
                    damage = flatModifier.TransformNegative(damage);
                }
            }

            if (attackType.AffectedByPercentModifier)
            {
                Stat percentModifier = GetStat(attackType.PercentModifier);
                if (percentModifier != null)
                {
                    damage = percentModifier.TransformNegative(damage);
                }
            }
            
            return damage;
        }

        public void IncreaseStat(int amount, StatType statType)
        {
            if(statType == healthStatType)
            {
                healthComponent.ModifyHealth(amount);
                return;
            }
            Stat stat = GetStat(statType);
            stat?.IncreaseValue(amount);
        }

        private Stat GetStat(StatType statType)
        {
            return stats.FirstOrDefault(stat => stat.StatType == statType);
        }
    }
}
