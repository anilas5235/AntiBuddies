using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IDamageDealer
    {
        void ApplyDamage(IDamageable target);
        GameObject GetGameObject();
    }
}