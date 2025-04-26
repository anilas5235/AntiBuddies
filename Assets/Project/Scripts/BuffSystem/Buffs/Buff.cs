using Project.Scripts.BuffSystem.Buffs.ExitBehaviour;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Buffs
{
    public sealed class Buff<T> : IBuff where T : EffectType
    {
        public string Name { get; }
        public GameObject Source => _effect.Source;
        public BuffManager BuffManager { get; private set; }
        private IStackBehaviour StackBehaviour { get; }
        private BuffGroup BuffGroup { get; set; }
        private ITarget<EffectPackage<T>> Target { get; }
        private readonly float _duration;
        private float _remainingDuration;
        private readonly EffectPackage<T> _effect;
        private readonly ITickBehaviour _tickBehavior;
        private readonly IExitBehaviour _exitBehavior;

        public Buff(EffectPackage<T> effect, float duration, ITarget<EffectPackage<T>> target, IStackBehaviour stackBehaviour,
            ITickBehaviour tickBehavior, IExitBehaviour exitBehavior)
        {
            Target = target;
            _exitBehavior = exitBehavior;
            _effect = effect;
            _duration = duration;
            _tickBehavior = tickBehavior;
            StackBehaviour = stackBehaviour;
            Name = ConstructName(effect, stackBehaviour, tickBehavior);
            ResetDuration();
        }

        private static string ConstructName(EffectPackage<T> effect, IStackBehaviour stackBehaviour,
            ITickBehaviour tickBehavior)
        {
            string name = $"{effect.EffectType.Name}_{stackBehaviour.Name}";
            if (tickBehavior != null) name += $"_{tickBehavior.Name}";
            return name;
        }

        public void OnBuffAdded()
        {
            OnBuffApply();
        }

        public void OnBuffTick(float deltaTime)
        {
            ReduceDuration(deltaTime);
            _tickBehavior?.OnBuffTick(this, deltaTime);

            if (IsBuffExpired()) RemoveBuff();
        }

        public void OnBuffApply() => Target.Apply(_effect);
        public void OnInverseBuffApply() => Target.Apply(_effect.Invert());

        public void OnBuffRemove() => _exitBehavior?.OnExit(this);

        public bool IsBuffExpired() => _remainingDuration <= 0;

        private void ResetDuration() => _remainingDuration = _duration;

        public void ReduceDuration(float amount)
        {
            _remainingDuration -= amount;
            _remainingDuration = Mathf.Max(0, _remainingDuration);
        }

        public void RegisteredAtBuffManager(BuffManager buffManager)
        {
            BuffManager = buffManager;
            StackBehaviour.AddingBuff(this, buffManager);
        }

        public void RegisteredAtBuffGroup(BuffGroup buffGroup) => BuffGroup = buffGroup;

        public void Refresh() => ResetDuration();

        public void RemoveBuff()
        {
            OnBuffRemove();
            BuffGroup.UnregisterBuff(this);
            BuffManager.RemoveBuffFromDictionary(this);
        }
    }
}