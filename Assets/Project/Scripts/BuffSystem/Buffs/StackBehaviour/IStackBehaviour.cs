using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    /// <summary>
    /// Defines the contract for stack behaviour logic for buffs.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IStackBehaviour
    {
        /// <summary>
        /// Determines whether the buff should be added to the buff manager.
        /// </summary>
        /// <param name="buff">The buff to potentially add.</param>
        /// <param name="buffManager">The buff manager handling the buffs.</param>
        /// <returns>True if the buff should be added; otherwise, false.</returns>
        bool ShouldBuffBeAdded(IBuff buff, BuffManager buffManager);
    }
}