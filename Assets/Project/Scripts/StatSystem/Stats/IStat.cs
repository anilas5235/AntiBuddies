namespace Project.Scripts.StatSystem.Stats
{
    public interface IStat
    {
        public int Value { get;}
        int TransformPositive(int baseValue);
        int TransformNegative(int baseValue);
    }
}