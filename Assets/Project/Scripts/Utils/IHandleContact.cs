using UnityEngine;

namespace Project.Scripts.Utils
{
    /// <summary>
    /// Interface for components that handle contact with other GameObjects.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IHandleContact
    {
        /// <summary>
        /// Handles logic when a contact with another GameObject occurs.
        /// </summary>
        /// <param name="contact">The contacted GameObject.</param>
        void HandleContact(GameObject contact);
    }
}