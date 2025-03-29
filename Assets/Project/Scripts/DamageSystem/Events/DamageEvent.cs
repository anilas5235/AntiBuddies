using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Events
{
    public class DamageEvent
    {
        public int DamageAmount { get; private set; }
        public DamageType DamageType { get; private set; }
        public IDamageDealer DamageDealer { get; private set; }
        public GameObject Target { get; private set; }

        public DamageEvent(int damageAmount, DamageType damageType, IDamageDealer damageDealer,
            GameObject target)
        {
            DamageAmount = damageAmount;
            DamageDealer = damageDealer;
            Target = target;
            DamageType = damageType;
        }
    }
}