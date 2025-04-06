using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public enum StatusType : byte
    {
        Stun,
        Slow,
        Weak,
        ArmorBreaking,
        Berserk,
        Vulnerable,
    }
    
    public static class StatusTypeExtensions
    {
        public static string GetName(this StatusType statusType)
        {
            return statusType switch
            {
                StatusType.Stun => "Stun",
                StatusType.Slow => "Slow",
                _ => "Unknown"
            };
        }
        
        public static Color GetColor(this StatusType statusType)
        {
            return statusType switch
            {
                StatusType.Stun => Color.yellow,
                StatusType.Slow => Color.blue,
                _ => Color.white
            };
        }
    }
}