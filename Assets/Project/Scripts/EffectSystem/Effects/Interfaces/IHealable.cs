using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable
    {
        int MaxHealth { get; }
        event Action<int,HealType,GameObject> OnHealApplied;
        event Action OnDeath;
        void ApplyHeal(int amount, HealType type);
        void FullHeal();
    }
}