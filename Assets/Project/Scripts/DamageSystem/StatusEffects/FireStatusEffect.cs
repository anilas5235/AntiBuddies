using UnityEngine;

namespace DamageSystem.StatusEffects
{
    public class FireStatusEffect : DamagingStatusEffect
    {
        public FireStatusEffect(IDamageable target, int fireDamagePerTick, float duration) 
            : base(target, new Attack() { FireDamage = fireDamagePerTick }, duration)
        { }
    }
}
