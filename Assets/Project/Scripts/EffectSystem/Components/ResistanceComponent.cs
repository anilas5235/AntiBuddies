using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ResistanceComponent : MonoBehaviour
    {
        [Header("Damage Resistance")] [SerializeField]
        private ClampedStat flatDamageReduction;

        [SerializeField] private List<SimpleKeyValuePair<EffectType, ClampedPercentStat>> resistances = new();

        public int ResistEffect(int value, AttackType effectType)
        {
            int result = value;
            if (effectType.AffectedByFlatModifier)
            {
                result = flatDamageReduction.TransformNegative(result);
            }

            if (effectType.AffectedByPercentModifier && TryGetResistance(effectType, out IStat resistance))
            {
                result = resistance.TransformNegative(result);
            }

            return result;
        }

        public bool IncreaseResistance(int value, EffectType effectType)
        {
            if (TryGetResistance(effectType, out IStat resistance))
            {
                resistance.IncreaseValue(value);
                return true;
            }
            return false;
        }

        private bool TryGetResistance(EffectType effectType, out IStat resistance)
        {
            resistance = resistances.FirstOrDefault(res => res.Key == effectType).Value;
            return resistance != null;
        }

        private void OnValidate()
        {
            flatDamageReduction.UpdateClampedValue();
            foreach (SimpleKeyValuePair<EffectType,ClampedPercentStat> pair in resistances)
            {
                pair.Value.UpdateClampedValue();
            }
        }
    }
}