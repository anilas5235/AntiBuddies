namespace Project.Scripts.EffectSystem.Components.Stats.StatBehaviour
{
    internal class FlatStatBehaviour : IStatBehaviour
    {
        public int TransformPositive(int statValue, int baseValue)
        {
            return Raise(statValue, baseValue);
        }

        public int TransformNegative(int statValue, int baseValue)
        {
            return Lower(statValue, baseValue);
        }

        private static int Raise(int stat, int baseValue)
        {
            return baseValue + stat;
        }

        private static int Lower(int stat, int baseValue)
        {
            return Raise(-stat, baseValue);
        }
    }
}