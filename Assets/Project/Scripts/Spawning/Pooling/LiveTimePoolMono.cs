using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public class LiveTimePoolableMono : PoolableMono
    {
        [SerializeField] protected float liveTime = 5f;
        [SerializeField] protected float timeToLive;

        private void FixedUpdate()
        {
            timeToLive -= Time.fixedDeltaTime;
            if (timeToLive <= 0)
            {
                ReturnToPool();
            }

            LiveTick();
        }

        protected virtual void LiveTick()
        {
        }

        public override void Reset()
        {
            timeToLive = liveTime;
        }
    }
}