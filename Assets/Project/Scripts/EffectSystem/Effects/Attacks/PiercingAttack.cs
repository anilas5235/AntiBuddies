using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : Attack
    {
        public PiercingAttack(GameObject source, float amount)
            : base(source, amount, EffectType.Piercing)
        {
        }

        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.PiercingResistance);
        }
    }
}