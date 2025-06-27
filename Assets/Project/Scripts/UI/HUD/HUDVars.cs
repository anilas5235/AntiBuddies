using UnityEngine;

namespace Project.Scripts.UI.HUD
{
    /// <summary>
    /// ScriptableObject holding HUD variable values for health, level, and gold.
    /// Used to update UI elements in the HUD using UI binding.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [CreateAssetMenu(fileName = "HUDVars", menuName = "UI/HUDVars")]
    public class HUDVars : ScriptableObject
    {
        /// <summary>
        /// Percentage value for the health bar (0-100).
        /// </summary>
        public float healthBar;

        /// <summary>
        /// Text representation of current health (e.g., "50/100").
        /// </summary>
        public string healthText;

        /// <summary>
        /// Percentage value for the level/experience bar (0-100).
        /// </summary>
        public float levelBar;

        /// <summary>
        /// Text representation of the current level (e.g., "Lvl.5").
        /// </summary>
        public string levelText;

        /// <summary>
        /// Current gold amount.
        /// </summary>
        public int gold;

        private int _level;

        /// <summary>
        /// Sets the current level and updates the level text.
        /// </summary>
        public int Level
        {
            set
            {
                _level = value;
                levelText = "Lvl." + _level;
            }
        }

        private int _health;
        private int _healthMax;

        /// <summary>
        /// Sets the current health and updates the health display.
        /// </summary>
        public int Health
        {
            set
            {
                _health = value;
                UpdateHealth();
            }
        }

        /// <summary>
        /// Sets the maximum health and updates the health display.
        /// </summary>
        public int HealthMax
        {
            set
            {
                _healthMax = value;
                UpdateHealth();
            }
        }

        /// <summary>
        /// Updates the health bar percentage and health text based on current and max health.
        /// </summary>
        private void UpdateHealth()
        {
            // Prevent division by zero and update health bar and text.
            healthBar = (float)_health / _healthMax * 100f;
            healthText = $"{_health}/{_healthMax}";
        }
    }
}