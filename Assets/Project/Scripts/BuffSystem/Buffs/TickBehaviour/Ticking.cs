using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    public class Ticking : ITickBehaviour
    {
        private const string ConstName = "Ticking";

        private float _timeSinceLastTick;
        private readonly float _tickInterval;
        public string Name => ConstName;
        
        public Ticking(float tickInterval)
        {
            _tickInterval = tickInterval;
        }

        public void OnBuffTick(IBuff buff, float deltaTime)
        {
            _timeSinceLastTick += deltaTime;
            if (!(_timeSinceLastTick >= _tickInterval)) return;
            int num = Mathf.FloorToInt(_timeSinceLastTick / _tickInterval);
            for (int i = 0; i < num; i++)
            {
                buff.OnBuffApply();
            }
            _timeSinceLastTick %= _tickInterval;
        }
    }
}