namespace DamageSystem.StatusEffects
{
    public abstract class DamagingStatusEffect : BaseStatusEffect
    {
        protected IDamageable Target;
        protected Attack DamagePerTick;
        
        public DamagingStatusEffect(IDamageable target, Attack damagePerTick, float duration) : base(duration)
        {
            Target = target;
            DamagePerTick = damagePerTick;
        }
        
        public override void Tick() => Target.TakeDamage(DamagePerTick.GetMultipliedAttack(StackCount));
    }
}