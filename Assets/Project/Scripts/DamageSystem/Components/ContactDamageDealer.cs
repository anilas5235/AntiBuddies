using System.Linq;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class ContactDamageDealer : DamageDealer
    {
        [SerializeField] private string[] targetTags = {"Player"};
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (targetTags.Any(other.CompareTag))
            {
                Attack(other.gameObject);
            }
        }
    }
}
