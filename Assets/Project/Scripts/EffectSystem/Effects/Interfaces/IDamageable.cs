using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        event Action<int,DamageType,GameObject> OnDamageReceived;
        void ApplyAttack(EffectPackage<DamageType> package);
        bool IsDead();

        bool IsAlive();

        void Die();
    }
}