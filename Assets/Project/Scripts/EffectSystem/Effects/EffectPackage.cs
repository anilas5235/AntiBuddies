using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public struct EffectPackage
    {
        public AlieGroup AlieGroup;
        public int Amount;
        public EffectType EffectType;
        public GameObject Source;
        
        public EffectPackage(AlieGroup alieGroup, int amount, EffectType effectType, GameObject source)
        {
            AlieGroup = alieGroup;
            Amount = amount;
            EffectType = effectType;
            Source = source;
        }
    }
}