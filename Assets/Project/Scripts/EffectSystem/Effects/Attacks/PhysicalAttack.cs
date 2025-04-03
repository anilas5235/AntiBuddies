using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PhysicalAttack : Attack
    {
        public PhysicalAttack(IDamageable target, IDamageDealer source, float amount) 
            : base(target, source, amount,EffectType.Physical)
        {
        }

        public override int CalculateDamage()
        {
            ResistanceData resData = Target.GetResistanceData();
            return CalculateDamage(resData.FlatDamageReduction, resData.PhysicalResistance);
        }
    }
}