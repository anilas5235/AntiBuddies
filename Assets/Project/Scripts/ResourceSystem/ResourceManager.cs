using System;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    /// <summary>
    /// Manages the player's gold resource and related events.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class ResourceManager : Singleton<ResourceManager>
    {
        /// <summary>
        /// Current amount of gold.
        /// </summary>
        [SerializeField] private int gold;

        /// <summary>
        /// Gets the current amount of gold.
        /// </summary>
        public int Gold => gold;

        /// <summary>
        /// Checks if the player has enough gold for a transaction.
        /// </summary>
        /// <param name="amount">Amount of gold to check.</param>
        /// <returns>True if the player has at least the specified amount of gold.</returns>
        public bool HasEnoughGold(int amount) => gold >= amount;

        /// <summary>
        /// Event invoked when the gold amount changes.
        /// </summary>
        public event Action OnGoldChange;

        /// <summary>
        /// Adds gold to the player's total and invokes the change event.
        /// </summary>
        /// <param name="amount">Amount of gold to add.</param>
        public void AddGold(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot add negative gold.");
                return;
            }

            gold += amount;
            OnGoldChange?.Invoke();
        }

        /// <summary>
        /// Removes gold from the player's total.
        /// </summary>
        /// <param name="amount">Amount of gold to remove.</param>
        public void RemoveGold(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Cannot remove negative gold.");
                return;
            }

            if (gold < amount)
            {
                Debug.LogWarning("Not enough gold to remove.");
                return;
            }

            gold -= amount;
            OnGoldChange?.Invoke();
        }
    }
}