namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public interface ITickBehaviour
    {
        string Name { get; }
        public void OnBuffAdded(IBuff buff);
        public void OnBuffTick(IBuff buff, float deltaTime);
        public void OnBuffRemove(IBuff buff);
    }
}