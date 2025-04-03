using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : Attack
    {
        public PiercingAttack(IDamageable target, IDamageDealer source, float amount) 
            : base(target, source, amount, EffectType.Piercing)
        {
        }

        public override int CalculateDamage()
        {
            ResistanceData resData = Target.GetResistanceData();
            return CalculateDamage(resData.FlatDamageReduction, resData.PiercingResistance);
        }
    }
}