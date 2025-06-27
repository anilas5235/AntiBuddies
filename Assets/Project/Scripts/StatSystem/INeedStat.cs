namespace Project.Scripts.StatSystem
{
    /// <summary>
    /// Interface for components that require stat group initialization.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface INeedStatGroup
    {
        /// <summary>
        /// Called to initialize stat references on components or script that need stats.
        /// </summary>
        /// <param name="statGroup">The stat Group to initialize from.</param>
        public void OnStatInit(IStatGroup statGroup);
    }
}