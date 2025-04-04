using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;

namespace Project.Scripts.BuffSystem.Components
{
    public abstract class TickingBuff<TTarget> : Buff<TTarget>
    {
        private float _timeSinceLastTick;
        private int _accumulatedTicks;
        private readonly float _tickInterval;

        protected TickingBuff(Effect<TTarget> effect, float duration, StackBehavior stackBehavior, TTarget target, float tickInterval) 
            : base(effect, duration, stackBehavior, target)
        {
            _tickInterval = tickInterval;
        }

        public override void OnBuffTick(float deltaTime)
        {
            base.OnBuffTick(deltaTime);
            _timeSinceLastTick += deltaTime;
            if (!(_timeSinceLastTick >= _tickInterval)) return;
            _accumulatedTicks++;
            _timeSinceLastTick = 0;
        }

        public override void OnBuffApply()
        {
            for (int i = 0; i < _accumulatedTicks; i++)
            {
                base.OnBuffApply();
            }
            _accumulatedTicks = 0;
        }

        public override void OnBuffAdded()
        {
            _accumulatedTicks++;
        }
    }
}