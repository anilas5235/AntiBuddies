using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public struct EffectPackage<T> where T : EffectType
    {
        public EffectDef<T> effectDef;
        public GameObject Source { get; }
        
        public int Amount => effectDef.Amount;
        public T EffectType => effectDef.EffectType;

        internal EffectPackage(int amount, T effectType, GameObject source)     
        {
            effectDef = new EffectDef<T>(amount, effectType);
            Source = source;
        }
        
        public readonly EffectPackage<T> Invert()
        {
            return new EffectPackage<T>(-effectDef.Amount, effectDef.EffectType, Source);
        }
    }
}