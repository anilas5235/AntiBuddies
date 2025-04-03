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

        public Attack ToAttack(IDamageable target, IDamageDealer source)
        {
            switch (effectType)
            {
                case EffectType.Physical:
                    return new PhysicalAttack(target, source, amount);
                case EffectType.Piercing:
                    return new PiercingAttack(target, source, amount);
                case EffectType.Fire:
                    return new FireAttack(target, source, amount);
            }
            throw new ArgumentException($"{effectType} effect type is not an a ApplyTo");
        }
    }
}