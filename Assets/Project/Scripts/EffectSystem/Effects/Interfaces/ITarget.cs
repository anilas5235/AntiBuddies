namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface ITarget<in TFor>
    {
        bool Apply(TFor applyable);
    }
}