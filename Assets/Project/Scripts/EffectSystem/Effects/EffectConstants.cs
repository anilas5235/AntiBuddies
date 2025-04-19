using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    public class EffectConstants
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Color Color;

        public EffectConstants(string name, string description, Color color)
        {
            Name = name;
            Description = description;
            Color = color;
        }
        
        public EffectConstants(string name, string description)
        {
            Name = name;
            Description = description;
            Color = Color.white;
        }
    }
}