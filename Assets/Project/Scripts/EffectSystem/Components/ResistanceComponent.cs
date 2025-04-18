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

        public int ResistEffect(EffectPackage effectPackage)
        {
            int result = effectPackage.Amount;
            if (effectPackage.EffectType.AffectedByFlatDamageReduction)
                result = flatDamageReduction.TransformNegative(result);

            if (TryGetResistance(effectPackage.EffectType, out IStat resistance))
                result = resistance.TransformNegative(result);

            return result;
        }

        private bool TryGetResistance(EffectType effectType, out IStat resistance)
        {
            resistance = resistances.FirstOrDefault(res => res.Key == effectType).Value;
            return resistance != null;
        }
    }
}