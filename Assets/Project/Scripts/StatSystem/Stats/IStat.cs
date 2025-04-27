namespace Project.Scripts.StatSystem.Stats
{
    public interface IStat
    {
        public int Value { get; set; }
        int TransformPositive(int baseValue);
        int TransformNegative(int baseValue);
    }
}