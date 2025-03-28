using System;
using UnityEngine.Serialization;

namespace DamageSystem
{
    [Serializable]
    public class Attack
    {
        public int NormalDamage;
        public int PiercingDamage;
        public DamageSender Sender;

        public Attack(int normalDamage, int piercingDamage = 0)
        {
            NormalDamage = normalDamage;
            PiercingDamage = piercingDamage;
        }
    }
}