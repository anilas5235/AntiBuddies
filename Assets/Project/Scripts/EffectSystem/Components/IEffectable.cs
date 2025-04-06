using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public interface IEffectable
    {
        void Apply(AttackInfo attackInfo, Component source);
        
        void Apply(List<AttackInfo> effectInfos, Component source);
    }
}