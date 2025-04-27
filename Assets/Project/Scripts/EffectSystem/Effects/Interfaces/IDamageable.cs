using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        event Action<int,AttackType,GameObject> OnDamageReceived;
        void ApplyAttack(EffectPackage<AttackType> package);
        bool IsDead();

        bool IsAlive();

        void Die();
    }
}