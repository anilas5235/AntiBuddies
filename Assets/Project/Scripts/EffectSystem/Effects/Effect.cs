using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<TTarget> : IEffect<TTarget>
    {
        protected readonly GameObject Source;
        protected Effect(GameObject source)
        {
            Source = source;
        }
        
        public abstract void Apply(TTarget target);
    }
}