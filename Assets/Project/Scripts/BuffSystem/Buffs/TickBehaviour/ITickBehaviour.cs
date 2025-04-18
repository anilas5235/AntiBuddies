namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public interface ITickBehaviour
    {
        string Name { get; }
        public void OnBuffTick(IBuff buff, float deltaTime);
    }
}