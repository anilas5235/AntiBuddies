using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class Buff : IBuff
    {
        private readonly float _duration;
        private float _remainingDuration;
        private readonly EffectPackage _effect;
        private readonly ITickBehaviour _tickBehavior;
        private readonly IStackBehaviour _stackBehaviour;
        public ITarget<EffectPackage> Target { get; }
        public GameObject Source=> _effect.Source;
        public BuffManager BuffManager { get; private set; }
        public BuffGroup BuffGroup { get; private set; }
        
        public IStackBehaviour StackBehaviour => _stackBehaviour;

        public string Name { get; }

        public Buff(EffectPackage effect, float duration, IStackBehaviour stackBehaviour, ITickBehaviour tickBehavior, ITarget<EffectPackage> target)
        {
            Target = target;
            _effect = effect;
            _duration = duration;
            _tickBehavior = tickBehavior;
            _stackBehaviour = stackBehaviour;
            Name = $"{_effect.EffectType.Name}_{_stackBehaviour.Name}_{_tickBehavior.Name}_Buff";
            ResetDuration();
        }
     
        public virtual void OnBuffAdded(){}

        public void OnBuffTick(float deltaTime)
        {
            _tickBehavior.Tick(this,deltaTime);
            
            if (!IsBuffExpired()) return;
            RemoveBuff();
        }

        public virtual void OnBuffApply() => Target.Apply(_effect);

        public virtual void OnBuffRemove() { }

        public bool IsBuffExpired() => _remainingDuration <= 0;

        private void ResetDuration() => _remainingDuration = _duration;
        
        public void ReduceDuration(float amount)
        {
            _remainingDuration -= amount;
            if (_remainingDuration < 0)
            {
                _remainingDuration = 0;
            }
        }

        public void RegisteredAtBuffManager(BuffManager buffManager)
        {
            BuffManager = buffManager;
            StackBehaviour.AddingBuff(this, buffManager);
        }

        public void RegisteredAtBuffGroup(BuffGroup buffGroup)
        {
            BuffGroup = buffGroup;
        }

        public void Refresh()
        {
            ResetDuration();
        }

        public void RemoveBuff()
        {
            BuffGroup.UnregisterBuff(this);
            BuffManager.RemoveBuffFromDictionary(this);
            OnBuffRemove();
        }
    }
}
