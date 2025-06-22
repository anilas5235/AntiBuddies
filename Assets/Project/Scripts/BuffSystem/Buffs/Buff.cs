using System;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Buffs
{
    /// <summary>
    /// Abstract base class for all buffs. Handles duration, stacking, ticking, and effect application.
    /// </summary>
    [Serializable]
    public abstract class Buff : IBuff
    {
        /// <summary>
        /// Constructs a buff name based on effect, stack, and tick behaviour.
        /// </summary>
        /// <param name="effectName">The name of the effect.</param>
        /// <param name="stackBehaviour">The stack behaviour instance.</param>
        /// <param name="tickBehaviour">The tick behaviour instance.</param>
        /// <returns>A string representing the buff.</returns>
        private static string ConstructName(string effectName, IStackBehaviour stackBehaviour,
            ITickBehaviour tickBehaviour)
        {
            string stackBehaviourName = stackBehaviour != null ? stackBehaviour.GetType().Name : "NoStackBehaviour";
            string tickBehaviourName = tickBehaviour != null ? tickBehaviour.GetType().Name : "NoTickBehaviour";
            return $"{effectName}_{stackBehaviourName}_{tickBehaviourName}";
        }

        /// <summary>
        /// Gets whether this buff affects allies.
        /// </summary>
        public bool AffectsAllies { get; }

        /// <summary>
        /// Gets the name of this buff.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the buff manager that manages this buff.
        /// </summary>
        public BuffManager BuffManager { get; private set; }

        /// <summary>
        /// Gets the stack behaviour for this buff.
        /// </summary>
        protected IStackBehaviour StackBehaviour { get; }

        /// <summary>
        /// Gets or sets the group this buff belongs to.
        /// </summary>
        public BuffGroup BuffGroup { get; set; }

        /// <summary>
        /// Gets or sets the hub for applying effects.
        /// </summary>
        public IPackageHub Hub { get; set; }

        /// <summary>
        /// Gets the total duration of the buff.
        /// </summary>
        protected float Duration { get; }

        /// <summary>
        /// Tracks the remaining duration of the buff.
        /// </summary>
        private float _remainingDuration;

        /// <summary>
        /// Gets the tick behaviour for this buff.
        /// </summary>
        protected ITickBehaviour TickBehavior { get; }

        /// <param name="effectName">The name of the effect.</param>
        /// <param name="duration">The duration of the buff.</param>
        /// <param name="hub">The hub for applying effects.</param>
        /// <param name="stack">The stack behaviour.</param>
        /// <param name="tick">The tick behaviour.</param>
        /// <param name="affectsAllies">Whether the buff affects allies.</param>
        protected Buff(string effectName, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick, bool affectsAllies)
        {
            Hub = hub;
            Duration = duration;
            TickBehavior = tick;
            AffectsAllies = affectsAllies;
            StackBehaviour = stack;
            Name = ConstructName(effectName, stack, tick);
            ResetDuration();
        }

        /// <inheritdoc/>
        public void OnBuffAdded()
        {
            OnBuffApply();
        }

        /// <inheritdoc/>
        public void OnBuffTick(float deltaTime)
        {
            ReduceDuration(deltaTime);
            TickBehavior?.OnBuffTick(this, deltaTime);

            // Remove the buff if it has expired.
            if (IsBuffExpired()) RemoveBuff();
        }

        /// <inheritdoc/>
        public virtual void OnBuffApply()
        {
            if (Hub == null)
            {
                throw new NullReferenceException($"Buff {Name} has no Hub to apply effects to.");
            }
        }

        /// <inheritdoc/>
        public virtual void OnBuffRemove()
        {
        }

        /// <inheritdoc/>
        public bool IsBuffExpired() => _remainingDuration <= 0;

        /// <summary>
        /// Resets the remaining duration to the full duration.
        /// </summary>
        private void ResetDuration() => _remainingDuration = Duration;

        /// <inheritdoc/>
        public void ReduceDuration(float amount) => _remainingDuration -= amount;

        /// <inheritdoc/>
        public bool ShouldBuffBeAdded(BuffManager buffManager)
        {
            BuffManager = buffManager;
            return StackBehaviour.ShouldBuffBeAdded(this, buffManager);
        }

        /// <inheritdoc/>
        public virtual void Refresh()
        {
            ResetDuration();
        }

        /// <inheritdoc/>
        public void RemoveBuff()
        {
            OnBuffRemove();
            BuffManager.RemoveBuff(this);
        }

        /// <inheritdoc/>
        public abstract IBuff GetCopy();
    }
}