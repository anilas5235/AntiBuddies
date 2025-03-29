using System;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    [Serializable]
    public abstract class DamageInfo : IDamage
    {
        public readonly int Damage;
        public readonly DamageType DamageType;

        protected DamageInfo(int damage, DamageType damageType)
        {
            Damage = damage;
            DamageType = damageType;
        }

        public abstract int CalcDamage(IResistance resistance);

        public DamageType GetDamageType() => DamageType;
        public int GetDamage() => Damage;
    }
}