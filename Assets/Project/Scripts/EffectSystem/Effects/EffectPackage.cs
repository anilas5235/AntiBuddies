using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public struct EffectPackage<T> where T : EffectType
    {
        public AlieGroup AlieGroup;
        public int Amount;
        public T EffectType;
        public GameObject Source;

        public EffectPackage(AlieGroup alieGroup, int amount, T effectType, GameObject source)
        {
            AlieGroup = alieGroup;
            Amount = amount;
            EffectType = effectType;
            Source = source;
        }
        
        public readonly EffectPackage<T> Invert()
        {
            return new EffectPackage<T>(AlieGroup, -Amount, EffectType, Source);
        }
    }
}