using System;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;
using UnityEngine.Serialization;

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

        public override string ToString() => amount.ToString();

        public Color GetColor() => color;
    }
}