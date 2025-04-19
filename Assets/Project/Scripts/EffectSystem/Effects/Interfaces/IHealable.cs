using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable
    {
        int MaxHealth { get; }
        event Action<int,EffectType,GameObject> OnHealApplied;
        event Action OnDeath;
        void ApplyHeal(int amount, EffectType type);
        void FullHeal();
    }
}