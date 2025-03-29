using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageDealer
    {
        void Attack(GameObject target, AttackPackage attackPackage);
    }

    // Erweiterungsmethode für IDamageDealer
    public static class DamageDealerExtensions
    {
        public static void ApplyDamage(this IDamageDealer dealer, GameObject target, AttackPackage attackPackage)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(attackPackage);
        }
    }
}
