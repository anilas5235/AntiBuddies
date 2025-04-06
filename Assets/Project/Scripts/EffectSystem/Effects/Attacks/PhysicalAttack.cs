using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PhysicalAttack : Attack
    {
        public PhysicalAttack(GameObject source, int amount) 
            : base(source, amount, AttackType.Physical)
        {
        }

        public override int CalculateDamage(ResistanceComponent resData)
        {
            return CalculateDamage(resData.flatDamageReduction, resData.physicalResistance);
        }
    }
}