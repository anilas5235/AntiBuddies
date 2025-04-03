using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    public enum EffectType : byte
    {
        Physical,
        Piercing,
        Healing,
        Fire,
    }
    
    public static class EffectTypeExtensions
    {
        public static string GetName(this EffectType effectType)
        {
            return effectType switch
            {
                EffectType.Physical => "Physical",
                EffectType.Piercing => "Piercing",
                EffectType.Fire => "Fire",
                EffectType.Healing => "Healing",
                _ => "Unknown"
            };
        }
        
        public static Color GetColor(this EffectType effectType)
        {
            return effectType switch
            {
                EffectType.Physical => Color.white,
                EffectType.Piercing => Color.cyan,
                EffectType.Healing => Color.green,
                EffectType.Fire => Color.red,
                _ => Color.white
            };
        }
        
        public static string GetDescription(this EffectType effectType){
            return effectType switch
            {
                EffectType.Physical => "Physical amount, reduced by armor.",
                EffectType.Piercing => "Piercing amount, ignores some armor.",
                EffectType.Healing => "Heals the target.",
                EffectType.Fire => "Fire amount, burns over time.",
                _ => "Unknown amount type."
            };
        }
    }
    
}