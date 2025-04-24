using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable
    {
        event Action<int,EffectType,GameObject> OnDamageReceived;
        void ApplyAttack(int amount, EffectType type);
        bool IsDead();

        bool IsAlive();

        void Die();
    }
}