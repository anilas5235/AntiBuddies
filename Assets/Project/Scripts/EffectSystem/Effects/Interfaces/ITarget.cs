namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface ITarget<in TFor>
    {
        void Apply(TFor applyable);
    }
}