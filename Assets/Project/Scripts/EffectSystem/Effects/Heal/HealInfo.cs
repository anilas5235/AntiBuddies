using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    [Serializable]
    public class HealInfo : IEffectInfo<IHeal,HealingType>
    {
        [SerializeField] private int amount;
        [SerializeField] private HealingType healingType;

        public HealInfo(int amount, HealingType healingType)
        {
            this.amount = amount;
            this.healingType = healingType;
        }
        
        public HealingType GetEffectType() => healingType;

        public int GetAmount() => amount;

        public Color GetColor() => healingType.GetColor();

        public IHeal ToEffect(GameObject source)
        {
            switch (healingType)
            {
                case HealingType.HealBurst: return new NormalHeal(source, amount);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}