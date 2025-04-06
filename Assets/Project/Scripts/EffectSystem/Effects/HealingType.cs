using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public enum HealingType
    {
        HealBurst,
    }

    public static class HealingTypeExtensions
    {
        public static string GetName(this HealingType healingType)
        {
            return healingType switch
            {
                HealingType.HealBurst => "Heal Burst",
                _ => "Unknown"
            };
        }

        public static Color GetColor(this HealingType healingType)
        {
            return healingType switch
            {
                _ => Color.green
            };
        }
    }
}