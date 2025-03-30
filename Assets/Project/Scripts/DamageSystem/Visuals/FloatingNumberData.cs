using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.DamageSystem.Visuals
{
    [Serializable]
    public class FloatingNumberData
    {
        [FormerlySerializedAs("damageInfo")] public EffectInfo effectInfo;
        public float lifeTime;
        
        public FloatingNumberData(EffectInfo effectInfo, float lifeTime)
        {
            this.effectInfo = effectInfo;
            this.lifeTime = lifeTime;
        }

        public override string ToString()
        {
            return effectInfo.amount.ToString();
        }
        
        public Color GetColor()
        {
            return effectInfo.effectType.GetColor();
        }
    }
}