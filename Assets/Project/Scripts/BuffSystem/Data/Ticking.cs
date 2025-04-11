using Project.Scripts.BuffSystem.Buffs;

namespace Project.Scripts.BuffSystem.Data
{
    public class Ticking : ITickBehavior
    {
        private float _timeSinceLastTick;
        private int _accumulatedTicks;
        private readonly float _tickInterval;
        public void Tick(float deltaTime, IBuff buff)
        {
            buff.ReduceDuration(deltaTime);
            _timeSinceLastTick += deltaTime;
            if (!(_timeSinceLastTick >= _tickInterval)) return;
            _accumulatedTicks++;
            _timeSinceLastTick = 0;
        }
    }
}