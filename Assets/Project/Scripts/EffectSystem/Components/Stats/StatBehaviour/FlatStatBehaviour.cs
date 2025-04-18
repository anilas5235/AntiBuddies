namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class FlatStatBehaviour : IStatBehaviour
    {
        private static int Raise(int stat, int baseValue) => baseValue + stat;

        private static int Lower(int stat, int baseValue) => Raise(-stat, baseValue);
        public int TransformPositive(int statValue, int baseValue) => Raise(statValue, baseValue);

        public int TransformNegative(int statValue, int baseValue) => Lower(statValue, baseValue);
    }
}