using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    [Serializable]
    public abstract class EffectPackage
    {
        [SerializeField] private int amount;
        public int Amount => amount;

        protected EffectPackage(int amount)
        {
            this.amount = amount;
        }
    }
}