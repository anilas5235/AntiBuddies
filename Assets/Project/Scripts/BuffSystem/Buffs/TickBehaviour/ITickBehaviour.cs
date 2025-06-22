namespace Project.Scripts.BuffSystem.Buffs.TickBehaviour
{
    /// <summary>
    /// Defines the contract for tick behaviour logic for buffs.
    /// </summary>
    public interface ITickBehaviour
    {
        /// <summary>
        /// Called every tick to update the buff logic.
        /// </summary>
        /// <param name="buff">The buff being updated.</param>
        /// <param name="deltaTime">The time elapsed since the last tick.</param>
        void OnBuffTick(IBuff buff, float deltaTime);
    }
}