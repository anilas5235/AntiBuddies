using System;
using Project.Scripts.EffectSystem.Effects.Status;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public interface IHealable
    {
        event Action<EffectPackage> OnHealApplied;
        event Action OnDeath;
        void ApplyHeal(int amount, EffectType type);
        void FullHeal();
        int MaxHealth { get;}
    }
}