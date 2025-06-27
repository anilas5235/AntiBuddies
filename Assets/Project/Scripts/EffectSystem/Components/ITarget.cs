namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Interface for applying a package or effect to a target.
    /// </summary>
    /// <typeparam name="TFor">The type of package or effect to apply.</typeparam>
    public interface ITarget<in TFor>
    {
        /// <summary>
        /// Applies the specified package or effect to the target.
        /// </summary>
        /// <param name="package">The package or effect to apply.</param>
        void Apply(TFor package);
    }
}