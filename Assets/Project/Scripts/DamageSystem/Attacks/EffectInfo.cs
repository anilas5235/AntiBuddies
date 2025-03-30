using System;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.DamageSystem.Attacks
{
    [Serializable]
    public class EffectInfo
    {
        public int amount;
        public EffectType effectType;

        public EffectInfo(int amount, EffectType effectType)
        {
            this.amount = amount;
            this.effectType = effectType;
        }

        public int CalcAmount(ResistanceData resistance)
        {
            float d = GetDamage();
            d -= resistance.GetFlatReduction(GetDamageType());
            d *= 1 - resistance.GetResistance(GetDamageType());
            return Mathf.RoundToInt(d);
        }

        public EffectType GetDamageType() => effectType;
        public int GetDamage() => amount;
    }
}