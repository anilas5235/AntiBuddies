using System;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable : ITarget<EffectPackage<HealType>>
    {
        event Action<int,HealType,GameObject> OnHealApplied;
        event Action OnDeath;
        void FullHeal();
    }
}