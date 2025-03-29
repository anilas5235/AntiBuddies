using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    public class FireStatusEffect : DamagingStatusEffect
    {
        public FireStatusEffect(IDamageable target, int fireDamagePerTick, float duration)
            : base(target, new FireAttack(fireDamagePerTick), duration)
        {
        }

        protected override AttackPackage GetAttack()
        {
            return new AttackPackage(new FireAttack(DamagePerTick.GetDamage() * StackCount));
        }
    }
}