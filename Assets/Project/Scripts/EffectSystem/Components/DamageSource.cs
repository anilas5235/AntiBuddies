using System;
using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class DamageSource : MonoBehaviour, IDamageDealer
    {
        public EffectInfo effectInfo = new(1, EffectType.Physical);

        public GameObject GetGameObject() => gameObject;

        protected Attack Attack;

        private void Awake()
        {
            Attack = effectInfo.ToAttack( gameObject);
        }

        public void ApplyDamage(IDamageable target)
        {
            target?.TakeDamage(Attack);
        }
    }
}