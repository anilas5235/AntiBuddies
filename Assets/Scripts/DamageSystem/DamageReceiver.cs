using UnityEngine;

namespace DamageSystem
{
    public class DamageReceiver : MonoBehaviour , IDamageable
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private int flatDamageReduction;
        [SerializeField,Range(0,1)] private float percentageDamageReduction;

        private void Awake()
        {
            healthComponent = GetComponent<HealthComponent>();
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0) return;
            damage -= flatDamageReduction;
            damage = Mathf.RoundToInt(damage * Mathf.Max(1 - percentageDamageReduction,0));
            healthComponent.TakeDamage(damage);
        }
    }
}