using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class DamageSource<T> : MonoBehaviour, IDamageDealer where T : IAttack
    {
        public EffectInfo<T> attackInfo;

        private IAttack _attack;

        private void Awake()
        {
            _attack = attackInfo.GetEffect(gameObject);
        }

        public void ApplyDamage(IDamageable target)
        {
            target?.ApplyDamage(_attack);
        }
    }
}