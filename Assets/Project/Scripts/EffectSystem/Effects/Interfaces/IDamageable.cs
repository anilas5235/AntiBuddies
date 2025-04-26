using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        event Action<int,AttackType,GameObject> OnDamageReceived;
        void ApplyAttack(int amount, AttackType type);
        bool IsDead();

        bool IsAlive();

        void Die();
    }
}