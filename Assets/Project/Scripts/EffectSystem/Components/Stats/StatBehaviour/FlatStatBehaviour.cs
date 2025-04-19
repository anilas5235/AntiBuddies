namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class FlatStatBehaviour : IStatBehaviour
    {
        public int TransformPositive(int statValue, int baseValue) => baseValue + statValue;
    }
}