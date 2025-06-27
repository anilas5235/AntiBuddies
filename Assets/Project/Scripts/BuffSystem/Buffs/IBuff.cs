using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs
{
    /// <summary>
    /// Interface for all buffs.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IBuff
    {
        /// <summary>
        /// Gets the buff manager that manages this buff.
        /// </summary>
        BuffManager BuffManager { get; }

        /// <summary>
        /// Gets or sets the group this buff belongs to.
        /// </summary>
        BuffGroup BuffGroup { get; set; }

        /// <summary>
        /// Gets or sets the hub for applying effects.
        /// </summary>
        public IPackageHub Hub { get; set; }

        /// <summary>
        /// Gets whether this buff affects allies.
        /// </summary>
        bool AffectsAllies { get; }

        /// <summary>
        /// Gets the name of this buff, which is constructed from the effect, stack behaviour, and tick behaviour.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Called when the buff is added to the manager.
        /// </summary>
        void OnBuffAdded();

        /// <summary>
        /// Called by the buffGroup this buff belongs to when it ticks.
        /// </summary>
        /// <param name="deltaTime">Time since last tick.</param>
        void OnBuffTick(float deltaTime);

        /// <summary>
        /// Applies the buff's effect.
        /// </summary>
        void OnBuffApply();

        /// <summary>
        /// Called when the buff is removed.
        /// </summary>
        void OnBuffRemove();

        /// <summary>
        /// Checks if the buff has expired.
        /// </summary>
        /// <returns>True if expired, otherwise false.</returns>
        bool IsBuffExpired();

        /// <summary>
        /// Reduces the remaining duration by the specified amount.
        /// </summary>
        /// <param name="amount">Amount to reduce.</param>
        void ReduceDuration(float amount);

        /// <summary>
        /// Determines if the buff should be added, based on stack behaviour.
        /// </summary>
        /// <param name="buffManager">The buff manager.</param>
        /// <returns>True if the buff should be added.</returns>
        bool ShouldBuffBeAdded(BuffManager buffManager);

        /// <summary>
        /// Refreshes the buff's duration.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Removes the buff from the group and manager.
        /// </summary>
        void RemoveBuff();

        /// <summary>
        /// Gets a copy of this buff.
        /// </summary>
        /// <returns>A new instance of the buff.</returns>
        IBuff GetCopy();
    }
}