using System;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    [Serializable]
    public class Attack
    {
        public int NormalDamage;
        public int PiercingDamage;
        public int FireDamage;
        public DamageSender Sender;
        
        public Attack GetMultipliedAttack(float multiplier)
        {
            return new Attack
            {
                NormalDamage = Mathf.FloorToInt(NormalDamage * multiplier),
                PiercingDamage = Mathf.FloorToInt(PiercingDamage * multiplier),
                FireDamage = Mathf.FloorToInt(FireDamage * multiplier),
                Sender = Sender
            };
        }
    }
}