using Project.Scripts.DamageSystem.Components;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    public static class DamageUtils
    {
        public static int CalcDamage(DamageInfo damageInfo,ResistanceData resistance)
        {
            float d = damageInfo.GetDamage();
            DamageType t = damageInfo.damageType;
            d -= resistance.GetFlatDamageReduction(t);
            d *= 1 - resistance.GetResistance(t);
            return Mathf.RoundToInt(d);
        }
        
        public static void Attack(GameObject target, DamageInfo damageInfo, Component attacker)
        {
            Attack(target.GetComponent<IDamageable>(), damageInfo,attacker);
        }
        
        public static void Attack(IDamageable target, DamageInfo damageInfo, Component attacker)
        {
            if (target == null || damageInfo == null) return;
            target.TakeDamage(damageInfo,attacker);
        }
    }
}