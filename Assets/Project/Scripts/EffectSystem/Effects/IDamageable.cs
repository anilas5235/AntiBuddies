using Project.Scripts.EffectSystem.Effects.Attacks;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IDamageable
    {
        void TakeDamage(Attack attack);
        
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}