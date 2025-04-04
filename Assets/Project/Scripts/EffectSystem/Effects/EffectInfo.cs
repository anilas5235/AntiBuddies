using System;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [Serializable]
    public class EffectInfo
    {
        [SerializeField] private int amount;
        [SerializeField] private EffectType effectType;

        public EffectInfo(int amount, EffectType effectType, bool isPercentage = false)
        {
            this.amount = amount;
            this.effectType = effectType;
        }

        public EffectType GetEffectType() => effectType;
        public int GetAmount() => amount;

        public Color GetColor() => effectType.GetColor();

        public Attack ToAttack(GameObject source)
        {
            switch (effectType)
            {
                case EffectType.Physical:
                    return new PhysicalAttack( source, amount);
                case EffectType.Piercing:
                    return new PiercingAttack( source, amount);
                case EffectType.Fire:
                    return new FireAttack( source, amount);
            }
            throw new ArgumentException($"{effectType} effect type is not an a ApplyTo");
        }
    }
}