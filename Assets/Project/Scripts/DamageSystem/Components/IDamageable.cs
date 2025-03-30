using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageable
    {
        void TakeDamage(DamageInfo damageInfo, Component attacker);
    }
}