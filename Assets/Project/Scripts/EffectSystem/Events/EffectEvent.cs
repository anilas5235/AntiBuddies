using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Events
{
    public struct EffectEvent
    {
        public EffectInfo Effect { get; private set; }
        public Component Source { get; private set; }
        public GameObject Target { get; private set; }

        public EffectEvent(EffectInfo effect, Component source,
            GameObject target)
        {
            Effect = effect;
            Source = source;
            Target = target;
        }
    }
}