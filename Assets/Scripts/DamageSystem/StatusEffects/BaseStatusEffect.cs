using System;

namespace DamageSystem.StatusEffects
{
    [Serializable]
    public abstract class BaseStatusEffect: IStatusEffect
    {
        public float Duration;
        public float RemainingDuration;
        public int StackCount;
        public int MaxStackCount;
        public bool Expired;
        
        public BaseStatusEffect(float duration)
        {
            Duration = duration;
            RemainingDuration = duration;
            Expired = false;
        }

        public abstract void Tick();
        
        public void Tick(float dt)
        {
            RemainingDuration -= dt;
            if (RemainingDuration <= 0)
            {
                Expired = true;
                RemainingDuration = 0;
            }
            else
            {
                Tick();
            }
        }
    }
}