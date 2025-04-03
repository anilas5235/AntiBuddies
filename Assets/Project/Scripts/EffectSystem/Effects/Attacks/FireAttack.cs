using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Resistance;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : Attack
    {
        public FireAttack(IDamageable target, IDamageDealer source, float amount) 
            : base(target, source, amount,EffectType.Fire)
        {
        }

        public override int CalculateDamage()
        {
            ResistanceData resData = Target.GetResistanceData();
            return CalculateDamage(resData.FlatDamageReduction, resData.FireResistance);
        }
    }
}