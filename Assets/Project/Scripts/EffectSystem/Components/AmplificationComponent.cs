using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class AmplificationComponent : MonoBehaviour
    {
        [SerializeField] private List<SimpleKeyValuePair<EffectType, PercentStat>> amplifications = new();

        public int AmplifyEffect(int value, EffectType effectType)
        {
            int result = value;
            if (TryGetAmplification(effectType, out IStat amplification))
                result = amplification.TransformPositive(result);

            return result;
        }

        private bool TryGetAmplification(EffectType type, out IStat amplification)
        {
            amplification = amplifications.FirstOrDefault(res => res.Key == type).Value;
            return amplification != null;
        }
    }
}