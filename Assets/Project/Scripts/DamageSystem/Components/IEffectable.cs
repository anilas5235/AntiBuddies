using System.Collections.Generic;
using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IEffectable
    {
        void Apply(EffectInfo effectInfo, Component source);
        
        void Apply(List<EffectInfo> effectInfos, Component source);
    }
}