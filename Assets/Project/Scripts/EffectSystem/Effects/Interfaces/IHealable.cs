using System;

namespace Project.Scripts.EffectSystem.Effects.Interfaces
{
    public interface IHealable
    {
        int Heal(int amount);
        event Action OnDeath;
        void FullHeal();
    }
}