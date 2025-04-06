using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<TTarget> : IEffect<TTarget>
    {
        protected readonly GameObject Source;
        private readonly int _amount;
        protected Effect(GameObject source, int amount)
        {
            Source = source;
            _amount = amount;
        }
        
        public abstract void Apply(TTarget target);
        public int GetAmount() => _amount;
    }
}