namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Interface for components that can be healed.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IHealable
    {
        /// <summary>
        /// Heals the component by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to heal.</param>
        /// <returns>The actual amount healed.</returns>
        int Heal(int amount);

        /// <summary>
        /// Fully heals the component to its maximum health.
        /// </summary>
        void FullHeal();
    }
}