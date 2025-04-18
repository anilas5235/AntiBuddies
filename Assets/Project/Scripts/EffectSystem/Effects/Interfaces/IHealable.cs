using System;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable
    {
        int MaxHealth { get; }
        event Action<EffectPackage> OnHealApplied;
        event Action OnDeath;
        void ApplyHeal(int amount, EffectType type);
        void FullHeal();
    }
}