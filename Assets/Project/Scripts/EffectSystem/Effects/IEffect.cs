namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffect<in TTarget>
    {
        public void Apply(TTarget target);
    }
}