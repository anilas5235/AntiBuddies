using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : Attack
    {
        public PiercingAttack(IDamageDealer source, float amount)
            : base(source, amount, EffectType.Piercing)
        {
        }

        public override int CalculateDamage(ResistanceData resData)
        {
            return CalculateDamage(resData.FlatDamageReduction, resData.PiercingResistance);
        }
    }
}