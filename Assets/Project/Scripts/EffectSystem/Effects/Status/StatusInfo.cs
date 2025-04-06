using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    [Serializable]
    public class StatusInfo : IEffectInfo<Status,StatusType>
    {
        [SerializeField] private int amount;
        [SerializeField] private StatusType statusType;

        public StatusInfo(int amount, StatusType statusType)
        {
            this.amount = amount;
            this.statusType = statusType;
        }

        public StatusType GetEffectType() => statusType;
        
        public int GetAmount() => amount;

        public Color GetColor() => statusType.GetColor();

        public Status ToEffect(GameObject source)
        {
            switch (statusType)
            {
                case StatusType.Stun:
                    break;
                case StatusType.Slow:
                    break;
                case StatusType.Weak:
                    break;
                case StatusType.ArmorBreaking:
                    break;
                case StatusType.Berserk:
                    break;
                case StatusType.Vulnerable:
                    break;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}