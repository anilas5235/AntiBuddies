using Project.Scripts.BuffSystem.Components;

namespace Project.Scripts.BuffSystem.Buffs.StackBehaviour
{
    /// <summary>
    /// Implements a stack behaviour where adding a buff refreshes the existing one if present.
    /// </summary>
    public class Refreshing : IStackBehaviour
    {
        /// <inheritdoc/>
        public bool ShouldBuffBeAdded(IBuff buff, BuffManager buffManager)
        {
            // If a buff with the same name exists, refresh it and do not add a new one.
            if (buffManager.TryGetFirstBuff(buff.Name, out IBuff presentBuff))
            {
                presentBuff.Refresh();
                return false;
            }

            // Otherwise, allow the new buff to be added.
            return true;
        }
    }
}