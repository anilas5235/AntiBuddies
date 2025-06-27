namespace Project.Scripts.ResourceSystem
{
    /// <summary>
    /// Interface for objects that can be picked up.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IPickUpable
    {
        /// <summary>
        /// Handles the logic for picking up the object.
        /// </summary>
        void PickUp();
    }
}