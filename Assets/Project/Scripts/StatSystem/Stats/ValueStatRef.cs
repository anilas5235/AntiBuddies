using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class ValueStatRef
    {
        [SerializeField] private List<StatRef> statRefs;
        [SerializeField] private float baseValue = 1f;
        [SerializeField] private float currValue;
        [SerializeField] private bool positiveTransform = true;
        public float CurrValue => currValue;
        public event Action OnValueChange;

        public float BaseValue
        {
            get => baseValue;
            set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Base value cannot be negative. Setting to 0.");
                    value = 0;
                }

                baseValue = value;
            }
        }

        public void Init(StatComponent statComponent)
        {
            foreach (StatRef statRef in statRefs)
            {
                statRef.Init(statComponent);
                statRef.Stat.OnStatChange += UpdateValue;
            }

            UpdateValue();
        }

        private void UpdateValue()
        {
            currValue = StatUtils.AggregateStatRefs(baseValue, statRefs, positiveTransform);
            OnValueChange?.Invoke();
        }

        public void AddStatRef(StatRef statRef)
        {
            if (statRef == null)
            {
                Debug.LogWarning("Cannot add null StatRef.");
                return;
            }

            statRefs.Add(statRef);
            statRef.Stat.OnStatChange += UpdateValue;
            UpdateValue();
        }
    }
}