using Project.Scripts.BuffSystem.Buffs;

namespace Project.Scripts.BuffSystem.Data
{
    public class NonTicking : ITickBehavior
    {
        public void Tick(float deltaTime, IBuff buff)
        {
            buff.ReduceDuration(deltaTime);
        }
    }
}