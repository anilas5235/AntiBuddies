using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    /// <summary>
    /// Interface for a hub that can receive and apply multiple types of effect packages and buffs.
    /// </summary>
    public interface IPackageHub : ITarget<DamagePackage>, ITarget<HealPackage>, ITarget<StatPackage>, IBuffTarget
    {
    }
}