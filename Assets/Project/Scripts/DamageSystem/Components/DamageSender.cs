using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageSender : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private Attack currAttack;

        public void Attack(GameObject target, Attack attack)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            attack.Sender = this;
            damageable?.TakeDamage(attack);
        }

        protected void Attack(GameObject target)
        {
            Attack(target, currAttack);
        }
    }
}
