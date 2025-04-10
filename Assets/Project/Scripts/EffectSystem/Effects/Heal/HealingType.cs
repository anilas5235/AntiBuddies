using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public enum HealingType
    {
        HealBurst,
        HealPercent,
        Vampiric,
    }

    public static class HealingTypeExtensions
    {
        public static string GetName(this HealingType healingType)
        {
            return healingType switch
            {
                HealingType.HealBurst => "NormalHeal Burst",
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