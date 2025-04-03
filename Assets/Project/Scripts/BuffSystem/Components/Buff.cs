using System;
using Project.Scripts.BuffSystem.Data;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    [Serializable]
    public abstract class Buff<T>: IBuff
    {
        [SerializeField] private BuffData buffData;
        [SerializeField] private float remainingDuration;
        [SerializeField] private float timeSinceLastTick;
        [SerializeField] private int accumulatedTicks;
        [SerializeField] protected T target;

        protected Buff(BuffData buffData, T target)
        {
            this.buffData = buffData;
            this.target = target;
            remainingDuration = buffData.Duration;
        }

        public virtual void OnBuffAdded()
        {
            accumulatedTicks++;
        }

        public virtual void OnBuffTick(float deltaTime)
        {
            remainingDuration -= deltaTime;
            
            if (buffData.TickBehavior != TickBehavior.Ticking || !(timeSinceLastTick >= buffData.TickInterval)) return;
            timeSinceLastTick += deltaTime;
            timeSinceLastTick -= buffData.TickInterval;
            accumulatedTicks++;
        }

        public virtual void OnBuffApply()
        {
            if (accumulatedTicks <= 0) return;
            for (int i = 0; i < accumulatedTicks; i++)
            {
                ExecuteEffect();
            }
            accumulatedTicks = 0;
        }
        
        protected abstract void ExecuteEffect();

        public virtual void OnBuffRemove()
        {
            
        }
        
        public void OnBuffRefresh()
        {
            if (buffData.StackBehavior == StackBehavior.Refresh) remainingDuration = buffData.Duration;
        }

        public bool IsBuffExpired() => remainingDuration <= 0;
    }
}