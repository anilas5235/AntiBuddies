using Project.Scripts.EffectSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PhysicalAttack : Attack
    {
        public PhysicalAttack(GameObject source, float amount) 
            : base(source, amount, AttackType.Physical)
        {
        }

        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.PhysicalResistance);
        }
    }
}