using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    /// <summary>
    /// Implements a stack behaviour where the buff cannot be stacked.
    /// </summary>
    public class NotStacking : IStackBehaviour
    {
        /// <inheritdoc/>
        public bool ShouldBuffBeAdded(IBuff buff, BuffManager buffManager)
        {
            // Only add the buff if it is not already present in the manager.
            return !buffManager.HasBuff(buff.Name);
        }
    }
}