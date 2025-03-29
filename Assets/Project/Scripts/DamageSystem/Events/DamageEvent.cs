using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.DamageSystem.Components;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Events
{
    public class DamageEvent
    {
        public int DamageAmount { get; private set; }
        public DamageType DamageType { get; private set; }
        public Vector3 Position { get; private set; }
        public IDamageDealer DamageDealer { get; private set; }
        public GameObject Target { get; private set; }

        public DamageEvent(int damageAmount, DamageType damageType, Vector3 position, IDamageDealer damageDealer,
            GameObject target)
        {
            DamageAmount = damageAmount;
            Position = position;
            DamageDealer = damageDealer;
            Target = target;
            DamageType = damageType;
        }
    }
}