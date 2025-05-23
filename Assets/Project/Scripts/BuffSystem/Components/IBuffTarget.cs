using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Components
{
    public interface IBuffTarget : ITarget<IBuff>, IAlieCheck
    {
    }
}