using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageReceiver : MonoBehaviour , IDamageable
    {
        private HealthComponent healthComponent;
        [SerializeField] private int flatDamageReduction;
        [SerializeField,Range(0,1)] private float percentageDamageReduction;

        private void Awake()
        {
            healthComponent = GetComponent<HealthComponent>();
        }

        public void TakeDamage(Attack attack)
        {
            int totalDamage = HandleNormalDamage(attack.NormalDamage);
            totalDamage += HandlePiercingDamage(attack.PiercingDamage);
            totalDamage = Mathf.Max(0, totalDamage);
            healthComponent.TakeDamage(totalDamage);
        }
        
        protected int HandleNormalDamage(int damage)
        {
            if (damage <= 0) return 0;
            damage -= flatDamageReduction;
            damage = Mathf.RoundToInt(damage * Mathf.Max(1 - percentageDamageReduction,0));
            return damage;
        }
        
        protected int HandlePiercingDamage(int damage)
        {
            if (damage <= 0) return 0;
            damage -= flatDamageReduction;
            return damage;
        }
    }
}