namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public class NonTicking : ITickBehavior
    {
        public void Tick(float deltaTime, IBuff buff)
        {
            buff.ReduceDuration(deltaTime);
        }
    }
}