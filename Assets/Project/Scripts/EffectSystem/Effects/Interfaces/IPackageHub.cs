using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IPackageHub : ITarget<DamagePackage>, ITarget<HealPackage>, ITarget<StatPackage>, IBuffTarget
    {
    }
}