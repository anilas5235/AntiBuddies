using System;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    [Serializable]
    public class DamageInfo
    {
        public int damage;
        public DamageType damageType;

        public DamageInfo(int damage, DamageType damageType)
        {
            this.damage = damage;
            this.damageType = damageType;
        }

        public int CalcDamage(ResistanceData resistance)
        {
            float d = GetDamage();
            d -= resistance.GetFlatDamageReduction(GetDamageType());
            d *= 1 - resistance.GetResistance(GetDamageType());
            return Mathf.RoundToInt(d);
        }

        public DamageType GetDamageType() => damageType;
        public int GetDamage() => damage;
    }
}