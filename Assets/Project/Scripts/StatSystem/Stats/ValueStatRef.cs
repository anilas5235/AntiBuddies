using System;
using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    [Serializable]
    public class ValueStatRef : StatRef
    {
        [SerializeField] private float baseValue = 1f;
        [SerializeField] private float currValue;
        [SerializeField] private bool positiveTransform = true;
        public float CurrValue => currValue;

        public override void Init(StatComponent statComponent)
        {
            base.Init(statComponent);
            Stat.OnStatChange += UpdateValue;
            UpdateValue();
        }

        ~ValueStatRef()
        {
            Stat.OnStatChange -= UpdateValue;
        }

        private void UpdateValue()
        {
            currValue = positiveTransform ? Stat.TransformPositive(baseValue) : Stat.TransformNegative(baseValue);
        }
    }
}
