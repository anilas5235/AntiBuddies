using UnityEngine;

namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class PercentStatBehaviour : IStatBehaviour
    {
        public int TransformPositive(int statValue, int baseValue)
            => Mathf.RoundToInt((1 + statValue / 100f) * baseValue);
    }
}