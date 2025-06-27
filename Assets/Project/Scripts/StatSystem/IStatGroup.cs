using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.StatSystem.Stats;

namespace Project.Scripts.StatSystem
{
    /// <summary>
    /// Interface for a group of stats, allowing retrieval and modification.
    /// </summary>
    public interface IStatGroup
    {
        /// <summary>
        /// Gets a stat by its type.
        /// </summary>
        /// <param name="statType">The type of the stat.</param>
        /// <returns>The stat instance, or null if not found.</returns>
        IStat GetStat(StatType statType);

        /// <summary>
        /// Modifies a stat using a StatPackage. If the stat does not exist, no action is taken.
        /// </summary>
        /// <param name="package">The modification package.</param>
        void ModifyStat(StatPackage package);
    }
}