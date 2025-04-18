namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        void ApplyAttack(int amount, EffectType type);
        bool IsDead();

        bool IsAlive();

        void Die();
    }
}