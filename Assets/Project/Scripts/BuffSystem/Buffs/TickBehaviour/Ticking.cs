namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public class Ticking : ITickBehaviour
    {
        private const string ConstName = "Ticking";

        private float _timeSinceLastTick;
        private int _accumulatedTicks;
        private readonly float _tickInterval;
        public string Name => ConstName;
        
        public Ticking(float tickInterval)
        {
            _tickInterval = tickInterval;
        }

        public void OnBuffAdded(IBuff buff)
        {
            _accumulatedTicks++;
        }

        public void OnBuffTick(IBuff buff, float deltaTime)
        {
            buff.ReduceDuration(deltaTime);
            _timeSinceLastTick += deltaTime;
            if (!(_timeSinceLastTick >= _tickInterval)) return;
            _accumulatedTicks++;
            _timeSinceLastTick = 0;
            for (int i = 0; i < _accumulatedTicks; i++)
            {
                buff.OnBuffApply();
            }
        }

        public void OnBuffRemove(IBuff buff)
        {
        }
    }
}