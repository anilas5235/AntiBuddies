namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public interface ITickBehaviour
    {
        string Name { get; }
        public void Tick(IBuff buff, float deltaTime);
    }
}