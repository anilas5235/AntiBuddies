using System;
using UnityEngine;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    [Serializable]
    public abstract class BaseStatusEffect: IStatusEffect
    {
        public float Duration;
        public float RemainingDuration;
        public int StackCount;
        public bool Expired;
        
        public BaseStatusEffect(float duration)
        {
            Duration = duration;
            RemainingDuration = duration;
            Expired = false;
        }

        public abstract void Tick(Component ticker);
        
        public void Tick(float dt,Component ticker)
        {
            RemainingDuration -= dt;
            if (RemainingDuration <= 0)
            {
                Expired = true;
                RemainingDuration = 0;
            }
            else
            {
                Tick(ticker);
            }
        }
    }
}