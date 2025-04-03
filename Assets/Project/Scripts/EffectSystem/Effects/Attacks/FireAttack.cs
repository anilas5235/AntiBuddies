using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;
using Unity.Cinemachine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : Attack
    {
        public FireAttack(IDamageDealer source, float amount)
            : base(source, amount, EffectType.Fire)
        {
        }


        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.FireResistance);
        }
    }
}