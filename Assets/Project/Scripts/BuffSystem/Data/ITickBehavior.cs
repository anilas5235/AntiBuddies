using Project.Scripts.BuffSystem.Buffs;

namespace Project.Scripts.BuffSystem.Data
{
    public interface ITickBehavior
    {
        public void Tick(float deltaTime, IBuff buff);
    }
}