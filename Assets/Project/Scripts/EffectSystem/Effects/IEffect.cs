using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffect<TTarget>
    {
        GameObject Source { get; }
        int Amount { get; }
        string Name { get; }
        string Description { get; }
        Color Color { get; }
        void Apply(TTarget target);
    }
}