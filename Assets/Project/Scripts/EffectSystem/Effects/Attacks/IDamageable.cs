namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public interface IDamageable
    {
        int ApplyDamage(IAttack attack);
        
        bool IsDead();
        
        bool IsAlive();
        
        void Die();
    }
}