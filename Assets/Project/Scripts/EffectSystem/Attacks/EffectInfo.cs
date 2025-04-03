using System;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Attacks
{
    [Serializable]
    public class EffectInfo
    {
        [SerializeField] private int amount;
        [SerializeField] private EffectType effectType;
        [SerializeField] private bool isPercentage;

        public EffectInfo(int amount, EffectType effectType, bool isPercentage = false)
        {
            this.amount = amount;
            this.effectType = effectType;
            this.isPercentage = isPercentage;
        }

        public int CalcAmount(ResistanceData resistance)
        {
            float d = GetAmount();
            d -= resistance.GetFlatReduction(GetEffectType());
            d *= 1 - resistance.GetResistance(GetEffectType());
            return Mathf.RoundToInt(d);
        }

        public EffectType GetEffectType() => effectType;
        public int GetAmount() => amount;

        public bool IsPercentage() => isPercentage;

        public Color GetColor() => effectType.GetColor();
    }
}