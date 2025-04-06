using System;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.EffectSystem.Components
{
    public class DamageSource : MonoBehaviour, IDamageDealer
    {
        public AttackInfo attackInfo = new(1, AttackType.Physical);

        private Attack _attack;

        private void Awake()
        {
            _attack = attackInfo.ToAttack(gameObject);
        }

        public void ApplyDamage(IDamageable target)
        {
            target?.TakeDamage(_attack);
        }
    }
}