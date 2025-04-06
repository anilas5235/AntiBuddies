namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageDealer
    {
        void ApplyDamage(IDamageable target);
    }
}