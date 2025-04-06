using Project.Scripts.EffectSystem.Components;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageable
    {
        void TakeDamage(Attack attack);

        ResistanceComponent GetResistance();
        
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}