using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public enum AttackType : byte
    {
        Physical,
        Piercing,
        Fire,
        Ice,
        Lightning,
        Poison,
        Bleed,
    }
    
    public static class AttackTypeExtensions
    {
        public static string GetName(this AttackType attackType)
        {
            return attackType switch
            {
                AttackType.Physical => "Physical",
                AttackType.Piercing => "Piercing",
                AttackType.Fire => "Fire",
                AttackType.Ice => "Ice",
                AttackType.Lightning => "Lightning",
                AttackType.Poison => "Poison",
                AttackType.Bleed => "Bleed",
                _ => "Error Unknown"
            };
        }
        
        public static Color GetColor(this AttackType attackType)
        {
            return attackType switch
            {
                AttackType.Fire => new Color(1f, 0.7f, 0.18f),
                AttackType.Ice => Color.cyan,
                AttackType.Lightning => Color.yellow,
                AttackType.Poison => new Color(0.83f, 0.48f, 1f),
                AttackType.Bleed => new Color(1f, 0.5f, 0.5f),
                _ => Color.white
            };
        }
    }
}