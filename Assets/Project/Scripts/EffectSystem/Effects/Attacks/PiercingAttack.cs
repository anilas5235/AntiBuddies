using Project.Scripts.EffectSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : Attack
    {
        public PiercingAttack(GameObject source, float amount)
            : base(source, amount, AttackType.Piercing)
        {
        }

        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.PiercingResistance);
        }
    }
}