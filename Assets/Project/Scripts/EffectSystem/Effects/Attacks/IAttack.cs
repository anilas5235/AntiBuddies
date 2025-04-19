using System;
using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IAttack : IEffect<IDamageable>
    {
        public int CalculateDamage(ResistanceComponent resistanceComponent);
        
        public static int CalculateDamage(int damage,Stat flatDamageReduction, PercentStat resistance)
        {
            // Apply flat damage reduction
            damage = flatDamageReduction.TransformNegative(damage);
            return damage <= 0 ? 0 : CalculateDamage(damage,resistance);
        }
        
        public static int CalculateDamage(int damage,PercentStat resistance)
        {
            // Apply resistance
            damage = resistance.TransformNegative(damage);
            return damage <= 0 ? 0 : Mathf.RoundToInt(damage);
        }
    }
}