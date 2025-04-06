namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffect<in TTarget>
    {
        void Apply(TTarget target);
        int GetAmount();
    }
}