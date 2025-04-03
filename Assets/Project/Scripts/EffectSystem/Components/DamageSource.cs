using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class DamageSource : MonoBehaviour, IDamageDealer
    {
        public EffectInfo effectInfo = new(1, EffectType.Physical);

        public GameObject GetGameObject() => gameObject;

        public void ApplyDamage(IDamageable target)
        {
            if (target == null) return;
            Attack attack = effectInfo.ToAttack(target, this);
            attack?.Apply();
        }
    }
}