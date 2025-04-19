using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffect<in TTarget> : IApplyable<TTarget>
    {
        EffectConstants EffectConstants { get; }
        GameObject Source { get; }
        int Amount { get; }
        string Name => EffectConstants.Name;
        string Description => EffectConstants.Description;
        Color Color => EffectConstants.Color;
    }
}