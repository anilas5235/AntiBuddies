using UnityEngine;

namespace Project.Scripts.UI
{
    [CreateAssetMenu(fileName = "HUDVars", menuName = "UI/HUDVars")]
    public class HUDVars : ScriptableObject
    {
        public float healthBar;
        public string healthText;
        public float levelBar;
        public string levelText;
        public int gold;

        private int _level;

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                levelText = "Lvl." + _level;
            }
        }

        private int _health;
        private int _healthMax;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                UpdateHealth();
            }
        }

        public int HealthMax
        {
            get => _healthMax;
            set
            {
                _healthMax = value;
                UpdateHealth();
            }
        }

        private void UpdateHealth()
        {
            healthBar = ((float)_health / _healthMax)*100f;
            healthText = $"{_health}/{_healthMax}";
        }
    }
}