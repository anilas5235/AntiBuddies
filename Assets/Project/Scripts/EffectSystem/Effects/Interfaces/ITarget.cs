namespace Project.Scripts.EffectSystem.Effects
{
    public interface ITarget<in TFor>
    {
        bool Apply(TFor applyable);
    }
}