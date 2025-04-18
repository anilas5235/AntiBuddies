namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public class NonTicking : ITickBehaviour
    {
        private const string ConstName = "NonTicking";

        public string Name => ConstName;
        public void OnBuffAdded(IBuff buff)
        {
            buff.OnBuffApply();
        }

        public void OnBuffTick(IBuff buff, float deltaTime)
        {
            buff.ReduceDuration(deltaTime);
        }

        public void OnBuffRemove(IBuff buff)
        {
            buff.OnInverseBuffApply();
        }
    }
}