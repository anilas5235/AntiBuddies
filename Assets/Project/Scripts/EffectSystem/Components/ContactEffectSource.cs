using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactEffectSource : EffectSource
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EffectRelay effectRelay)) HandleContact(effectRelay);
        }

        private void HandleContact(EffectRelay effectRelay)
        {
            if (effectRelay.AlieGroup == alieGroup) return;
            ApplyEffect(effectRelay);
        }
    }
}