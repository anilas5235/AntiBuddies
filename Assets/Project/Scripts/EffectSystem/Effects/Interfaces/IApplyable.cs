namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IApplyable<in TTarget>
    {
        void Apply(TTarget applyTarget);
    }
}