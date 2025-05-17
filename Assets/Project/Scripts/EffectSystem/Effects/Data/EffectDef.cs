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
        [SerializeField] private List<StatDependency> extraStatDeps;
        public int Amount => amount;
        public T EffectType => effectType;

        public EffectDef(int amount, T effectType)
        {
            this.amount = amount;
            this.effectType = effectType;
            extraStatDeps = new List<StatDependency>();
        }

        public EffectPackage<T> CreatePackage(GameObject source, IStatGroup statComponent)
        {
            int finalAmount = amount;
            if (statComponent != null)
            {
                finalAmount = effectType.CreationScale(amount, statComponent, extraStatDeps);
            }
            return new EffectPackage<T>(finalAmount, effectType, source);
        }
    }
}
