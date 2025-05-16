using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public struct EffectDef<T> where T : EffectType
    {
        [SerializeField] private int amount;
        [SerializeField] private T effectType;
        public int Amount => amount;
        public T EffectType => effectType;

        public EffectDef(int amount, T effectType)
        {
            this.amount = amount;
            this.effectType = effectType;
        }

        public EffectPackage<T> CreatePackage(GameObject source, StatComponent statComponent, List<StatType> extraStats =null)
        {
            int finalAmount = amount;
            if (statComponent)
            {
                List<IStat> extraStatList = new();
                if (extraStats != null)
                {
                    extraStatList.AddRange(extraStats.Select(statComponent.GetStat).Where(stat => stat != null));
                }
                finalAmount = effectType.CreationScale(amount, statComponent, extraStatList);
            }
            return new EffectPackage<T>(finalAmount, effectType, source);
        }
    }
}
