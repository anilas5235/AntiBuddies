using System;
using Project.Scripts.EffectSystem.Components.Stats.StatBehaviour;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Scripts.EffectSystem.Components.Stats
{
    [Serializable]
    public class BaseStat : IStat
    {
        [SerializeField] private int statValue;
        public UnityEvent<int> OnChanged;
        private IStatBehaviour _statBehaviour;

        internal BaseStat(IStatBehaviour statBehaviour, int statValue)
        {
            _statBehaviour = statBehaviour;
        }

        public BaseStat() : this(new FlatStatBehaviour(), 0)
        {
        }

        public BaseStat(int statValue) : this(new FlatStatBehaviour(), statValue)
        {
        }

        protected int StatValue
        {
            get => statValue;
            set
            {
                if (statValue == value) return;
                statValue = value;
                OnStatValueChanged();
                OnChange?.Invoke(statValue);
                OnChanged?.Invoke(statValue);
            }
        }

        public bool IsBelowOrZero()
        {
            return StatValue <= 0;
        }

        public virtual void ReduceValue(int amount)
        {
            StatValue -= amount;
        }

        public virtual void IncreaseValue(int amount)
        {
            StatValue += amount;
        }

        public virtual int TransformPositive(int baseValue)
        {
            return _statBehaviour.TransformPositive(StatValue, baseValue);
        }

        public virtual int TransformNegative(int baseValue)
        {
            return _statBehaviour.TransformNegative(StatValue, baseValue);
        }

        public event Action<int> OnChange;

        protected virtual void OnStatValueChanged()
        {
        }
    }
}