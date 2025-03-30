using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Events
{
    public struct DamageEvent
    {
        public DamageInfo Damage { get; private set; }
        public Component Source { get; private set; }
        public GameObject Target { get; private set; }

        public DamageEvent(DamageInfo damage, Component source,
            GameObject target)
        {
            Damage = damage;
            Source = source;
            Target = target;
        }
    }
}