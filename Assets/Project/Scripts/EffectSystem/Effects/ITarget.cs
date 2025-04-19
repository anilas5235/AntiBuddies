namespace Project.Scripts.EffectSystem.Effects
{
    public interface ITarget<in TFor>
    {
        void Apply(TFor applyable);
    }
}