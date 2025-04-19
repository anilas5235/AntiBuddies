namespace Project.Scripts.EffectSystem.Components.Stats
{
    public interface IStat
    {
        int TransformPositive(int baseValue);
        int TransformNegative(int baseValue);
        void ReduceValue(int amount);
        void IncreaseValue(int amount);
        bool IsBelowOrZero();
    }
}