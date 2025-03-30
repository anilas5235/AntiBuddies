using System;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Visuals
{
    [Serializable]
    public class FloatingNumberData
    {
        public DamageInfo damageInfo;
        public float lifeTime;
        
        public FloatingNumberData(DamageInfo damageInfo, float lifeTime)
        {
            this.damageInfo = damageInfo;
            this.lifeTime = lifeTime;
        }

        public override string ToString()
        {
            return damageInfo.damage.ToString();
        }
        
        public Color GetColor()
        {
            return damageInfo.damageType.GetColor();
        }
    }
}