using System.Linq;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactDamageSource : DamageSource
    {
        [SerializeField] private string[] targetTags = {"Player"};
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (targetTags.Any(other.CompareTag) && other.TryGetComponent(out EffectRelay effectRelay))
            {
                HealthComponent health = effectRelay.HealthComponent;
                if (health)ApplyDamage(health);
            }
        }
    }
}
