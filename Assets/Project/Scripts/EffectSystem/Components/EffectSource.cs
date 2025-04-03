using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class EffectSource : MonoBehaviour, IEffectSource
    {
        public EffectInfo effectInfo = new(1, EffectType.Physical);

        public void Attack(GameObject target)
        {
            DamageUtils.Attack(target, effectInfo, this);
        }
    }
}
