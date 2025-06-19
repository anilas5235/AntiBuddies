namespace Project.Scripts.StatSystem
{
    public interface INeedStatComponent 
    {
        /// <summary>
        /// Called on child components of the stat component in hierarchy to initialize their stat references.
        /// </summary>
        /// <param name="statComponent">The stat component to initialize from.</param>
        public void OnStatInit(StatComponent statComponent);
    }
}