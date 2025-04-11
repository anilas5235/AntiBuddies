using Project.Scripts.BuffSystem.Components;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class Buff<TTarget> : IBuff
    {
        private readonly float _duration;
        private float _remainingDuration;
        private readonly IEffect<TTarget> _effect;
        private ITickBehavior _tickBehavior;
        public TTarget Target { get; }
        public GameObject Source=> _effect.Source;
        public BuffManager BuffManager { get; set; }

        public string Name { get; }

        public Buff(IEffect<TTarget> effect, float duration, IStackBehavior stackBehavior, ITickBehavior tickBehavior, TTarget target)
        {
            Target = target;
            _effect = effect;
            _duration = duration;
            _tickBehavior = tickBehavior;
            Name = _effect.Name + "_Buff";
            ResetDuration();
        }

        public virtual void OnBuffAdded(){}

        public void OnBuffTick(float deltaTime)
        {
            _tickBehavior.Tick(deltaTime, this);
        }

        public virtual void OnBuffApply() => _effect.Apply(Target);

        public virtual void OnBuffRemove(){}

        public bool IsBuffExpired() => _remainingDuration <= 0;
        
        protected void ResetDuration() => _remainingDuration = _duration;
        
        public void ReduceDuration(float amount)
        {
            _remainingDuration -= amount;
            if (_remainingDuration < 0)
            {
                _remainingDuration = 0;
            }
        }
    }
}
