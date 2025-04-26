using System;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable
    {
        event Action<int,HealType,GameObject> OnHealApplied;
        event Action OnDeath;
        void ApplyHeal(int amount, HealType type);
        void FullHeal();
    }
}