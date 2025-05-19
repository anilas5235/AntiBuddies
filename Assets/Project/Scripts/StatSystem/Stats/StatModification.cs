using Project.Scripts.EffectSystem.Effects.Type;

namespace Project.Scripts.StatSystem.Stats
{
    public record StatModification
    {
        public StatType StatType;
        public Type ModType;
        public int Value;

        public StatModification(StatType statType, int value, Type modType)
        {
            StatType = statType;
            Value = value;
            ModType = modType;
        }

        public enum Type
        {
            BaseValue,
            TempValue,
            MaxValue,
            MinValue
        }

        public StatModification Inverse() => new(StatType, -Value, ModType);
    }
}