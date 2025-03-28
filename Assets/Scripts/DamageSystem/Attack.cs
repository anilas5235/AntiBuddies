using System;

namespace DamageSystem
{
    [Serializable]
    public class Attack
    {
        public int NormalDamage;
        public int PiercingDamage;
        public int FireDamage;
        public DamageSender Sender;
    }
}