using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    /// <summary>
    /// PoolableMono that automatically returns itself to the pool after a set lifetime.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class LiveTimePoolableMono : PoolableMono
    {
        [SerializeField] protected float lifeTime = 5f;
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

        /// <summary>
        /// Called every FixedUpdate while the object is alive. Override for custom logic.
        /// </summary>
        protected virtual void LiveTick()
        {
        }

        /// <summary>
        /// Resets the object's lifetime.
        /// </summary>
        public override void Reset()
        {
            timeToLive = lifeTime;
        }
    }
}