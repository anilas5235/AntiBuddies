namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageable
    {
        void TakeDamage(Attack attack);
        
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}