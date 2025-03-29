using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Events
{
    public struct DamageEvent
    {
        public int DamageAmount { get; private set; }
        public DamageType DamageType { get; private set; }
        public Component Source { get; private set; }
        public GameObject Target { get; private set; }

        public DamageEvent(int damageAmount, DamageType damageType, Component source,
            GameObject target)
        {
            DamageAmount = damageAmount;
            Source = source;
            Target = target;
            DamageType = damageType;
        }
    }
}