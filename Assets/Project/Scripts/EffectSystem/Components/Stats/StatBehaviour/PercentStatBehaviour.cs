using UnityEngine;

namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class PercentStatBehaviour : IStatBehaviour
    {
        public int TransformPositive(int statValue, int baseValue)
        {
            return RaiseBy(statValue, baseValue);
        }

        public int TransformNegative(int statValue, int baseValue)
        {
            return LowerBy(statValue, baseValue);
        }

        private static int RaiseBy(int percent, int baseValue)
        {
            return Mathf.RoundToInt((1 + percent / 100f) * baseValue);
        }

        private static int LowerBy(int percent, int baseValue)
        {
            return RaiseBy(-percent, baseValue);
        }
    }
}