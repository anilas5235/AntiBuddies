using UnityEngine;

namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class PercentStatBehaviour : IStatBehaviour
    {
        private static int RaiseBy(int percent, int baseValue)
        {
            return Mathf.RoundToInt((1+percent/100f) * baseValue);
        }
        
        private static int LowerBy(int percent, int baseValue)
        {
            return RaiseBy(-percent, baseValue);
        }
        
        public int TransformPositive(int statValue, int baseValue) => RaiseBy(statValue, baseValue);

        public int TransformNegative(int statValue, int baseValue) => LowerBy(statValue, baseValue);
    }
}