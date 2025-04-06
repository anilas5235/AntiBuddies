using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : Attack
    {
        public PiercingAttack(GameObject source, int amount)
            : base(source, amount, AttackType.Piercing)
        {
        }

        public override int CalculateDamage(ResistanceComponent resData)
        {
            return CalculateDamage(resData.flatDamageReduction, resData.piercingResistance);
        }
    }
}