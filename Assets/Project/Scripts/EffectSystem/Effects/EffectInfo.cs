using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [Serializable]
    public class EffectInfo<TEffect>
    {
        public int amount;

        public EffectInfo(int amount)
        {
            this.amount = amount;
        }

        public TEffect GetEffect(GameObject source)
        {
            return (TEffect)Activator.CreateInstance(typeof(TEffect),source ,amount);
        }
    }
}