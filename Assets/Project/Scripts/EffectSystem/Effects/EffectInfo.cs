using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffectInfo<out TEffect>
    {
        public TEffect GetEffect(GameObject source);
    }
}