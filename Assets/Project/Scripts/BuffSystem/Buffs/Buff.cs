using System;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;

namespace Project.Scripts.BuffSystem.Buffs
{
    [Serializable]
    public abstract class Buff : IBuff
    {
        protected static string ConstructName(EffectType effectType, IStackBehaviour stackBehaviour,
            ITickBehaviour tickBehaviour)
        {
            string stackBehaviourName = stackBehaviour != null ? stackBehaviour.GetType().Name : "NoStackBehaviour";
            string tickBehaviourName = tickBehaviour != null ? tickBehaviour.GetType().Name : "NoTickBehaviour";
            return $"{effectType.Name}_{stackBehaviourName}_{tickBehaviourName}";
        }

        public bool AffectsAllies { get; }
        public string Name { get; }
        public BuffManager BuffManager { get; private set; }
        protected IStackBehaviour StackBehaviour { get; }
        public BuffGroup BuffGroup { get; set; }
        public IPackageHub Hub { get; set; }
        protected float Duration  { get; }
        private float _remainingDuration;
        protected ITickBehaviour TickBehavior  { get; }

        protected Buff(string buffName, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick, bool affectsAllies)
        {
            Hub = hub;
            Duration = duration;
            TickBehavior = tick;
            AffectsAllies = affectsAllies;
            StackBehaviour = stack;
            Name = buffName;
            ResetDuration();
        }

        public void OnBuffAdded()
        {
            OnBuffApply();
        }

        public void OnBuffTick(float deltaTime)
        {
            ReduceDuration(deltaTime);
            TickBehavior?.OnBuffTick(this, deltaTime);

            if (IsBuffExpired()) RemoveBuff();
        }

        public virtual void OnBuffApply()
        {
            if (Hub == null)
            {
                throw new NullReferenceException($"Buff {Name} has no Hub to apply effects to.");
            }
        }

        public virtual void OnBuffRemove()
        {
        }

        public bool IsBuffExpired() => _remainingDuration <= 0;

        private void ResetDuration() => _remainingDuration = Duration;

        public void ReduceDuration(float amount) => _remainingDuration -= amount;

        public bool ShouldBuffBeAdded(BuffManager buffManager)
        {
            BuffManager = buffManager;
            return StackBehaviour.ShouldBuffBeAdded(this, buffManager);
        }

        public virtual void Refresh()
        {
            ResetDuration();
        }

        public void RemoveBuff()
        {
            OnBuffRemove();
            BuffGroup.UnregisterBuff(this);
            BuffManager.RemoveBuffFromDictionary(this);
        }

        public abstract IBuff GetCopy();
    }
}