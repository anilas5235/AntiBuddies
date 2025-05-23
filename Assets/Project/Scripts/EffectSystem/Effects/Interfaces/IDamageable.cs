namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        int TakeDamage(int amount);
        bool IsDead();
        bool IsAlive();
        void Die();
    }
}