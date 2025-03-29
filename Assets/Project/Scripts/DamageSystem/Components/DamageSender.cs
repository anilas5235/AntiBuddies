using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageSender : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private Attack currAttack;

        void IDamageDealer.Attack(GameObject target, Attack attack)
        {
            IDamageable comp = target.GetComponent<IDamageable>();
            attack.Sender = this;
            comp?.TakeDamage(attack);
        }
    }
}