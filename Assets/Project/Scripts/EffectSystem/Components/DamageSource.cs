using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public abstract class DamageSource : MonoBehaviour, IDamageDealer, IEffectInfo<IAttack>
    {
        [SerializeField] private int amount;

        [SerializeField] private AttackType type;

        private enum AttackType
        {
            Physical,
            Piercing,
            Fire,
            Ice,
            Lightning,
            Poison,
            Bleed,
        }

        private IAttack _attack;

        private void Awake()
        {
            _attack = GetEffect(gameObject);
        }

        public void ApplyDamage(IDamageable target)
        {
            target?.ApplyDamage(_attack);
        }

        public IAttack GetEffect(GameObject source)
        {
            switch (type)
            {
                case AttackType.Physical:
                    return new PhysicalAttack(source, amount);
                case AttackType.Piercing:
                    return new PiercingAttack(source, amount);
                case AttackType.Fire:
                    return new FireAttack(source, amount);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}