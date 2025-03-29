using System;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Events;
using Project.Scripts.DamageSystem.Resistance;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    [RequireComponent(typeof(HealthComponent))]
    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        // Event for damage received
        public event Action<DamageEvent> OnDamageReceived;
        
        private HealthComponent _healthComponent;
        [Header("Damage Reduction")]
        [SerializeField] protected ResistanceData resistances;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
        }

        public void TakeDamage(AttackPackage attackPackage)
        {
            if (attackPackage == null || attackPackage.DamageComponents == null) return;
            
            int totalDamage = 0;
            foreach (IDamage damageInfo in attackPackage.DamageComponents)
            {
                int damage = damageInfo.CalcDamage(resistances);
                if (damage <= 0) continue;
                
                totalDamage += damage;
                SendDamageEvent(damage, damageInfo.GetDamageType(), attackPackage.Sender);
            }
            
            // Apply the total damage to the health component
            if(totalDamage > 0) _healthComponent.TakeDamage(totalDamage);
        }

        private void SendDamageEvent(int damage, DamageType damageType, IDamageDealer damageSource)
        {
            // Create a new DamageEvent and invoke the event
            DamageEvent damageEvent = new(
                damage, 
                damageType,
                damageSource, 
                gameObject
            );
            OnDamageReceived?.Invoke(damageEvent);
        }
    }
}
