namespace Project.Scripts.WeaponSystem
{
    /// <summary>
    /// Interface for all weapon types.
    /// </summary>
    public interface IWeapon
    {
        /// <summary>
        /// Executes the weapon's attack action.
        /// </summary>
        void Attack();

        /// <summary>
        /// Destroys the weapon and performs any necessary cleanup.
        /// </summary>
        void DestroyWeapon();
    }
}