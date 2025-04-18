using System.Collections.Generic;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ResistanceComponent : MonoBehaviour
    {
        [Header("Damage Resistance")]
        public ClampedStat flatDamageReduction;
        public Dictionary<EffectType,ClampedStat> Resistances = new ();


        public int ResistEffect(EffectPackage effectPackage)
        {
            int result = effectPackage.Amount;
            if (effectPackage.EffectType.AffectedByFlatDamageReduction)
            {
                result = flatDamageReduction.TransformNegative(result);
            }

            if (Resistances.TryGetValue(effectPackage.EffectType, out ClampedStat resistance))
            {
                result = resistance.TransformNegative(result);
            }
            return result;
        }
    }
}