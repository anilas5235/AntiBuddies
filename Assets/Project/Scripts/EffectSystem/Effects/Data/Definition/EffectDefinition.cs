using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Definition
{
    [Serializable]
    public abstract record EffectDefinition
    {
        [SerializeField] protected int amount;
    }
}