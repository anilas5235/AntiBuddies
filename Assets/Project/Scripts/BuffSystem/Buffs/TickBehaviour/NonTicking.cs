namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public class NonTicking : ITickBehaviour
    {
        private const string ConstName = "NonTicking";

        public string Name => ConstName;

        public void Tick(IBuff buff, float deltaTime)
        {
            buff.ReduceDuration(deltaTime);
        }
    }
}