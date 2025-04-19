namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal interface IStatBehaviour
    {
        int TransformPositive(int statValue, int baseValue);
        int TransformNegative(int statValue, int baseValue);
    }
}