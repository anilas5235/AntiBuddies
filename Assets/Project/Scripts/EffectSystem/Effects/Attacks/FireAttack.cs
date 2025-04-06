using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : Attack
    {
        public FireAttack(GameObject source, int amount) 
            : base(source, amount, AttackType.Fire) 
        {
        }

        public override int CalculateDamage(ResistanceComponent resData)
        {
            return CalculateDamage(resData.flatDamageReduction, resData.fireResistance);
        }
    }
}