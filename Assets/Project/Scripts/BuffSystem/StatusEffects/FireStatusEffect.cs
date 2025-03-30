using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    public class FireStatusEffect : DamagingStatusEffect
    {
        public FireStatusEffect(IDamageable target, int fireDamagePerTick, float duration)
            : base(target, new DamageInfo(fireDamagePerTick, DamageType.Fire), duration)
        {
        }

        protected override DamageInfo GetAttack()
        {
            return new DamageInfo(DamagePerTick.GetDamage() * StackCount, DamagePerTick.GetDamageType());
        }
    }
}