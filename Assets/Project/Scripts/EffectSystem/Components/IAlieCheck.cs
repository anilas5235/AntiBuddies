namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Interface for checking if a group is considered an ally.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IAlieCheck
    {
        /// <summary>
        /// Determines if the specified group is an ally.
        /// </summary>
        /// <param name="group">The allied group to check.</param>
        /// <returns>True if the group is an ally; otherwise, false.</returns>
        public bool IsAlie(AlliedGroup group);
    }
}