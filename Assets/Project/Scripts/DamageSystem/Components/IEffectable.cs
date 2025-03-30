using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IEffectable
    {
        void TakeDamage(EffectInfo effectInfo, Component attacker);
    }
}