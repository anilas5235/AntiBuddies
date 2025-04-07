using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public abstract class Effect<TTarget> : IEffect<TTarget>
    {
        public GameObject Source { get; }

        public int Amount { get; }

        protected Effect(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        
        public abstract void Apply(TTarget target);
        public int GetAmount() => Amount;
    }
}