using Project.Scripts.DamageSystem.Components;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    public static class DamageUtils
    {
        public static int CalcDamage(EffectInfo effectInfo,ResistanceData resistance)
        {
            float d = effectInfo.GetAmount();
            EffectType t = effectInfo.GetEffectType();
            d -= resistance.GetFlatReduction(t);
            d *= 1 - resistance.GetResistance(t);
            return Mathf.RoundToInt(d);
        }
        
        public static void Attack(GameObject target, EffectInfo effectInfo, Component attacker)
        {
            Attack(target.GetComponent<IEffectable>(), effectInfo,attacker);
        }
        
        public static void Attack(IEffectable target, EffectInfo effectInfo, Component attacker)
        {
            if (target == null || effectInfo == null) return;
            target.TakeDamage(effectInfo,attacker);
        }
    }
}