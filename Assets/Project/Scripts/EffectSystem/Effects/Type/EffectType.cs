using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    public abstract class EffectType : ScriptableObject
    {
        [SerializeField] private string description = "no description jet";
        public string Name => name;
        public string Description => description;
    }
}