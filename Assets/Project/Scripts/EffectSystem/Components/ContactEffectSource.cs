using System.Collections;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactEffectSource : EffectSource
    {
        private const float CooldownTime = 0.5f;
            
        [SerializeField] private AlieGroup alieGroup;
        
        private Coroutine _cooldownCoroutine;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EffectRelay effectRelay))
            {
                HandleContact(effectRelay);
            }
        }

        private void HandleContact(EffectRelay effectRelay)
        {
            if(_cooldownCoroutine != null || effectRelay.AlieGroup == alieGroup) return;
            ApplyEffect(effectRelay);
            _cooldownCoroutine = StartCoroutine(CoolDown());
        }

        private IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(CooldownTime);
        }
    }
}