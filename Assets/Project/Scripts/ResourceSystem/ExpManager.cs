using System;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.ResourceSystem
{
    public class ExpManager : Singleton<ExpManager>
    {
        private const int BaseExpPerLevel = 25;
        [SerializeField] private int exp;
        [SerializeField] private int level;
        [SerializeField] private int expToLevelUp = 1;
        [SerializeField] private int expLevelUpStepPercentage = 50;

        public float ExpProgress => (float)exp / expToLevelUp;

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

        public event Action OnExpGain;
        public event Action OnLevelUp;

        protected override void Awake()
        {
            base.Awake();
            Reset();
        }

        public void AddExp(int amount)
        {
            Exp += amount;
            if (Exp >= expToLevelUp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Exp -= expToLevelUp;
            Level++;
            SetExpToLevelUp(Level);
            Debug.Log($"Level Up! New Level: {Level}, Exp to next level: {expToLevelUp}");
        }

        private void SetExpToLevelUp(int lvl)
        {
            expToLevelUp = lvl < 1
                ? BaseExpPerLevel
                : Mathf.RoundToInt(expToLevelUp * (1f + expLevelUpStepPercentage / 100f * lvl));
        }

        private void Reset()
        {
            Level = 0;
            Exp = 0;
            SetExpToLevelUp(Level);
        }
    }
}