namespace Project.Scripts.EffectSystem.Effects.Status
{
    public interface IStatusEffectable
    {
        void Apply(IStatus target);
    }
}