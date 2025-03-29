using Project.Scripts.DamageSystem.Attacks;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageable
    {
        void TakeDamage(AttackPackage attackPackage);
    }
}