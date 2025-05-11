using System;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public struct EffectDef<T> where T : EffectType
    {
        [SerializeField] private int amount;
        [SerializeField] private T effectType;
        public int Amount => amount;
        public T EffectType => effectType;

        public EffectDef(int amount, T effectType)
        {
            this.amount = amount;
            this.effectType = effectType;
        }

        public EffectPackage<T> CreatePackage(GameObject source, StatComponent statComponent)
        {
            int finalAmount = amount;
            if (statComponent)
            {
                finalAmount = effectType.CreationScale(amount, statComponent);
            }
            return new EffectPackage<T>(finalAmount, effectType, source);
        }
    }
}
