using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;

namespace Project.Scripts.EffectSystem.ExtraEffects
{
    /// <summary>
    /// Interface for extra effect components that can be added to an effect handler.
    /// </summary>
    public interface IExtraEffect
    {
        /// <summary>
        /// Determines if this extra effect should be applied.
        /// </summary>
        /// <param name="mode">The trigger mode currently being processed.</param>
        /// <returns>True if the extra effect should be applied, otherwise false.</returns>
        bool ApplyCondition(ExtraEffectHandler.TriggerType mode);

        /// <summary>
        /// Applies the extra effect to the given package hub and stat group.
        /// </summary>
        /// <param name="hub">The package hub to apply the effect to.</param>
        /// <param name="statGroup">The stat group to use.</param>
        void ApplyTo(IPackageHub hub, IStatGroup statGroup);
    }
}