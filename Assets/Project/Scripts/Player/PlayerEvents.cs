using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Player
{
    /// <summary>
    /// Handles player-related events such as death.
    /// </summary>
    public class PlayerEvents : MonoBehaviour
    {
        /// <summary>
        /// Called when the player dies. Triggers the end run menu.
        /// </summary>
        public void OnDeath()
        {
            UIManager.Instance.ShowEndRunMenu();
        }
    }
}