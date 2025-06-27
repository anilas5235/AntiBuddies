using System;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    /// <summary>
    /// Manages player experience points (EXP), level progression, and related events.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class ExpManager : Singleton<ExpManager>
    {
        /// <summary>
        /// The base experience required for the first level up.
        /// </summary>
        private const int BaseExpPerLevel = 25;

        /// <summary>
        /// Current experience points.
        /// </summary>
        [SerializeField] private int exp;

        /// <summary>
        /// Current player level.
        /// </summary>
        [SerializeField] private int level;

        /// <summary>
        /// Experience required to reach the next level.
        /// </summary>
        [SerializeField] private int expToLevelUp = 1;

        /// <summary>
        /// Percentage increase in required experience per level.
        /// </summary>
        [SerializeField] private int expLevelUpStepPercentage = 50;

        /// <summary>
        /// Progress towards the next level as a value between 0 and 1.
        /// </summary>
        public float ExpProgress => (float)exp / expToLevelUp;

        /// <summary>
        /// Current player level. Cannot be set to a negative value.
        /// </summary>
        public int Level
        {
            get => level;
            private set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Level cannot be negative. Setting to 0.");
                    value = 0;
                }

                level = value;
                OnLevelUp?.Invoke();
            }
        }

        /// <summary>
        /// Current experience points. Cannot be set to a negative value.
        /// </summary>
        public int Exp
        {
            get => exp;
            private set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Experience cannot be negative. Setting to 0.");
                    value = 0;
                }

                exp = value;
                OnExpGain?.Invoke();
            }
        }

        /// <summary>
        /// Event invoked when experience is gained.
        /// </summary>
        public event Action OnExpGain;

        /// <summary>
        /// Event invoked when the player levels up.
        /// </summary>
        public event Action OnLevelUp;

        protected override void Awake()
        {
            base.Awake();
            Reset();
        }

        /// <summary>
        /// Adds experience points and handles level up if threshold is reached.
        /// </summary>
        /// <param name="amount">Amount of experience to add.</param>
        public void AddExp(int amount)
        {
            Exp += amount;
            if (Exp >= expToLevelUp)
            {
                LevelUp();
            }
        }

        /// <summary>
        /// Handles leveling up, resets experience, increases level, and recalculates required experience.
        /// </summary>
        private void LevelUp()
        {
            Exp -= expToLevelUp;
            Level++;
            SetExpToLevelUp(Level);
            Debug.Log($"Level Up! New Level: {Level}, Exp to next level: {expToLevelUp}");
        }

        /// <summary>
        /// Calculates the required experience for the next level based on the current level.
        /// </summary>
        /// <param name="lvl">Current level.</param>
        private void SetExpToLevelUp(int lvl)
        {
            expToLevelUp = lvl < 1
                ? BaseExpPerLevel
                // Increase required EXP by a percentage for each level.
                : Mathf.RoundToInt(expToLevelUp * (1f + expLevelUpStepPercentage / 100f * lvl));
        }

        /// <summary>
        /// Resets level and experience to initial values.
        /// </summary>
        private void Reset()
        {
            Level = 0;
            Exp = 0;
            SetExpToLevelUp(Level);
        }
    }
}