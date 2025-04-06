using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    [Serializable]
    public class HealInfo : IEffectInfo<Heal,HealingType>
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

        public Heal ToEffect(GameObject source)
        {
            switch (healingType)
            {
                case HealingType.HealBurst: return new HealBurst(source, amount);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}