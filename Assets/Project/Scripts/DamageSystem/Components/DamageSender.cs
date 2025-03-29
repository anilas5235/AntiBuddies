using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageSender : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private AttackPackage _currAttackPackage;

        public void Attack(GameObject target, AttackPackage attackPackage)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(attackPackage);
        }

        protected void Attack(GameObject target)
        {
            Attack(target, _currAttackPackage);
        }
    }
}
