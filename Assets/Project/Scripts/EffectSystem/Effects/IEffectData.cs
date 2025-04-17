using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public interface IEffectData<out TEffect>
    {
        public TEffect GetEffect(GameObject source);
    }
}