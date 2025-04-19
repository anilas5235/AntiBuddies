namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageable : ITarget<IAttack>
    {
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}