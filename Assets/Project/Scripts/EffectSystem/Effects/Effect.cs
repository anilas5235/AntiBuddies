using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<TTarget>
    {
        protected readonly GameObject Source;
        private readonly EffectType _effectType;
        protected Effect(GameObject source, EffectType effectType)
        {
            Source = source;
            _effectType = effectType;
        }
        
        public abstract void Apply(TTarget target);
        
        public EffectType GetEffectType() => _effectType;
    }
}