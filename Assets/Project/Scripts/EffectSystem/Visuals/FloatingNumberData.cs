using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    [Serializable]
    public class FloatingNumberData
    {
        public int amount;
        public Color color;
        public float lifeTime;

        public FloatingNumberData(int amount, Color color, float lifeTime)
        {
            this.amount = amount;
            this.color = color;
            this.lifeTime = lifeTime;
        }

        public override string ToString()
        {
            return amount.ToString();
        }

        public Color GetColor()
        {
            return color;
        }
    }
}