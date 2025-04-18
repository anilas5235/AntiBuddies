using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Components.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IAttack : IEffect<IDamageable>
    {
        public int CalculateDamage(ResistanceComponent resistanceComponent);
        
        public static int CalculateDamage(int damage,IStat flatDamageReduction, IStat resistance)
        {
            // Apply flat damage reduction
            damage = flatDamageReduction.TransformNegative(damage);
            return damage <= 0 ? 0 : CalculateDamage(damage,resistance);
        }
        
        public static int CalculateDamage(int damage,IStat resistance)
        {
            // Apply resistance
            damage = resistance.TransformNegative(damage);
            return damage <= 0 ? 0 : Mathf.RoundToInt(damage);
        }
    }
}