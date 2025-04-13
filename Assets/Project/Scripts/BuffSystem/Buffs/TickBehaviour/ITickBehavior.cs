namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public interface ITickBehavior
    {
        public void Tick(float deltaTime, IBuff buff);
    }
}