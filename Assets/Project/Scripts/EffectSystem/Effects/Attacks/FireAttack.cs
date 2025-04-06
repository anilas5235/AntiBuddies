using Project.Scripts.EffectSystem.Resistance;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : Attack
    {
        public FireAttack(GameObject source, float amount) 
            : base(source, amount, AttackType.Fire) 
        {
        }

        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.FireResistance);
        }
    }
}