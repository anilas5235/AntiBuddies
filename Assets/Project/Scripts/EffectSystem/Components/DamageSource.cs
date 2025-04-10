using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class DamageSource : MonoBehaviour, IDamageDealer
    {
        public EffectInfo attackInfo = new();

        private IAttack _attack;

        private void Awake()
        {
            ///_attack = attackInfo.ToEffect(gameObject);
        }

        public void ApplyDamage(IDamageable target)
        {
            target?.ApplyDamage(_attack);
        }
    }
}