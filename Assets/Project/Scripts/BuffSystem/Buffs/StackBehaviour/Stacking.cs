using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    /// <summary>
    /// Implements a stack behaviour where buffs can always be stacked.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class Stacking : IStackBehaviour
    {
        /// <inheritdoc/>
        public bool ShouldBuffBeAdded(IBuff buff, BuffManager buffManager)
        {
            // Always allow stacking of buffs.
            return true;
        }
    }
}