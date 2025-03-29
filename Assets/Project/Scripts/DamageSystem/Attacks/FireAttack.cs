using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Attacks
{
    public class FireAttack : DamageInfo
    {
        public FireAttack(int damage) : base(damage, DamageType.Fire)
        {
        }

        public override int CalcDamage(IResistance resistance)
        {
            float damage = Damage;
            damage *= 1 - resistance.GetResistance(DamageType.Fire);
            return Mathf.RoundToInt(damage);
        }
    }
}