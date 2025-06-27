namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Interface for components that can take damage and have a life/death state.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Applies damage to the component.
        /// </summary>
        /// <param name="amount">The amount of damage to apply.</param>
        /// <returns>The actual damage taken.</returns>
        int TakeDamage(int amount);

        /// <summary>
        /// Checks if the component is dead.
        /// </summary>
        /// <returns>True if dead; otherwise, false.</returns>
        bool IsDead();

        /// <summary>
        /// Checks if the component is alive.
        /// </summary>
        /// <returns>True if alive; otherwise, false.</returns>
        bool IsAlive();

        /// <summary>
        /// Kills the component.
        /// </summary>
        void Die();
    }
}