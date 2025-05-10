using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IDamageable : ITarget<EffectPackage<DamageType>>
    {
        event Action<int,DamageType,GameObject> OnDamageReceived;
        bool IsDead();
        bool IsAlive();

        void Die();
    }
}