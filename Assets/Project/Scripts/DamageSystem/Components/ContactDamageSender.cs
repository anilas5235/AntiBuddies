using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class ContactDamageSender : DamageSender
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Attack(other.gameObject);
            }
        }
    }
}
