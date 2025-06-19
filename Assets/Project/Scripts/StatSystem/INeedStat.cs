namespace Project.Scripts.StatSystem
{
    public interface INeedStatGroup 
    {
        /// <summary>
        /// Called on child components of the stat component in hierarchy to initialize their stat references.
        /// </summary>
        /// <param name="statGroup">The stat Group to initialize from.</param>
        public void OnStatInit(IStatGroup statGroup);
    }
}