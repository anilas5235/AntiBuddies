using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    public enum DamageType : byte
    {
        Physical,
        Piercing,
        Fire,
    }
    
    public static class DamageTypeExtensions
    {
        public static string GetName(this DamageType damageType)
        {
            return damageType switch
            {
                DamageType.Physical => "Physical",
                DamageType.Piercing => "Piercing",
                DamageType.Fire => "Fire",
                _ => "Unknown"
            };
        }
        
        public static Color GetColor(this DamageType damageType)
        {
            return damageType switch
            {
                DamageType.Physical => Color.white,
                DamageType.Piercing => Color.cyan,
                DamageType.Fire => Color.red,
                _ => Color.white
            };
        }
        
        public static string GetDescription(this DamageType damageType){
            return damageType switch
            {
                DamageType.Physical => "Physical damage, reduced by armor.",
                DamageType.Piercing => "Piercing damage, ignores some armor.",
                DamageType.Fire => "Fire damage, burns over time.",
                _ => "Unknown damage type."
            };
        }
    }
    
}