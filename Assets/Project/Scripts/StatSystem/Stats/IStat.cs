namespace Project.Scripts.StatSystem.Stats
{
    public interface IStat
    {
        public int Value { get;}
        public int TransformPositive(int baseValue);
        public int TransformNegative(int baseValue);
        public float TransformPositive(float baseValue);
        public float TransformNegative(float baseValue);
    }
}