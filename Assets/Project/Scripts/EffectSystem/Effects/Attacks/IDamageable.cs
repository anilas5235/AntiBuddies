using Project.Scripts.EffectSystem.Effects.Status;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageable
    {
        void ApplyAttack(int amount, EffectType type);
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}