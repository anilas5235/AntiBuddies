namespace Project.Scripts.EffectSystem.Effects
{
    public interface IApplyable<in TTarget>
    {
        void Apply(TTarget applyTarget);
    }
}