using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;

namespace Project.Scripts.BuffSystem.Components
{
    public abstract class Buff<TTarget> : IBuff
    {
        private readonly float _duration;
        private float _remainingDuration;
        private readonly TTarget _target;
        private readonly Effect<TTarget> _effect;
        public StackBehavior StackBehavior { get; private set; }

        protected Buff(Effect<TTarget> effect, float duration, StackBehavior stackBehavior, TTarget target)
        {
            _target = target;
            StackBehavior = stackBehavior;
            _effect = effect;
            _duration = duration;
            ResetDuration();
        }

        public virtual void OnBuffAdded(){}

        public virtual void OnBuffTick(float deltaTime)
        {
            _remainingDuration -= deltaTime;
        }

        public virtual void OnBuffApply() => _effect.Apply(_target);

        public virtual void OnBuffRemove(){}

        public bool IsBuffExpired() => _remainingDuration <= 0;
        
        protected void ResetDuration() => _remainingDuration = _duration;
    }
}
