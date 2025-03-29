using System.Collections.Generic;
using System.Linq;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageSender : MonoBehaviour, IDamageDealer
    {
        [SerializeField] protected List<DamageInfo> damageInfos = new List<DamageInfo>();

        public void Attack(GameObject target, AttackPackage attackPackage)
        {
            var damageable = target.GetComponent<IDamageable>();
            if (damageable != null)
            {
                // Verknüpfe den Sender mit dem Angriff
                attackPackage.SetSender(this);
                damageable.TakeDamage(attackPackage);
            }
        }

        protected void Attack(GameObject target)
        {
            var attackPackage = new AttackPackage(damageInfos);
            Attack(target, attackPackage);
        }
    }
}
