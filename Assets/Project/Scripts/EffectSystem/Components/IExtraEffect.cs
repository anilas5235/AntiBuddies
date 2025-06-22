using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;

namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Interface for extra effect components that can be added to an effect handler.
    /// </summary>
    public interface IExtraEffect
    {
        /// <summary>
        /// Determines if this extra effect should be applied for the given trigger mode.
        /// </summary>
        /// <param name="mode">The trigger mode.</param>
        /// <returns>True if the extra effect should be applied, otherwise false.</returns>
        bool ShouldAdd(EffectTrigger mode);
        /// <summary>
        /// Applies the extra effect to the given package hub and stat group.
        /// </summary>
        /// <param name="hub">The package hub to apply the effect to.</param>
        /// <param name="statGroup">The stat group to use.</param>
        void ApplyTo(IPackageHub hub, IStatGroup statGroup);
    }
}