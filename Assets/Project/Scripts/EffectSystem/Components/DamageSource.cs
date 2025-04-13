using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class DamageSource : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private AttackData attack;

        public void ApplyDamage(IDamageable target)
        {
            target?.Apply(attack.GetEffect(gameObject));
        }
    }
}