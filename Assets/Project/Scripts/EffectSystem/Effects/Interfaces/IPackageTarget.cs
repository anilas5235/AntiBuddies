using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IPackageTarget<T> : ITarget<EffectPackage<T>> where T : EffectType
    {
        public bool IsAlie(AlieGroup group);
    }
}