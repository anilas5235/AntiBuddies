using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class Buff<TTarget> : IBuff<TTarget>
    {
        private readonly float _duration;
        private float _remainingDuration;
        private readonly Effect<TTarget> _effect;
        public StackBehavior StackBehavior { get; }
        public TTarget Target { get; }
        public GameObject Source=> _effect.Source;

        public Buff(Effect<TTarget> effect, float duration, StackBehavior stackBehavior, TTarget target)
        {
            Target = target;
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

        public virtual void OnBuffApply() => _effect.Apply(Target);

        public virtual void OnBuffRemove(){}

        public bool IsBuffExpired() => _remainingDuration <= 0;
        
        protected void ResetDuration() => _remainingDuration = _duration;
    }
}
