using UnityEngine;

namespace Project.Scripts.DamageSystem.StatusEffects
{
    public interface IStatusEffect
    {
        void Tick(float dt,Component ticker);
    }
}