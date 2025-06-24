using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Components;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// Interface for a target that can receive buffs and supports ally checking.
    /// </summary>
    public interface IBuffTarget : ITarget<IBuff>, IAlieCheck
    {
    }
}