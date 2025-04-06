namespace Project.Scripts.EffectSystem.Effects
{
    public interface IDamageDealer
    {
        void ApplyDamage(IDamageable target);
    }
}