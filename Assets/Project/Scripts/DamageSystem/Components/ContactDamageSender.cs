using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class ContactDamageSender : DamageSender
    {
        [SerializeField] private string[] targetTags = {"Player"};
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (var tag in targetTags)
            {
                if (other.CompareTag(tag))
                {
                    Attack(other.gameObject);
                    break;
                }
            }
        }
    }
}
