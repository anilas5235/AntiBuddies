using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;

namespace Project.Scripts.EffectSystem.Components
{
    public interface IEffectPipelineComponent
    {
        bool ShouldAdd(EffectPipelineMode mode);
        void ApplyTo(IPackageHub hub, IStatGroup statGroup);
    }
}