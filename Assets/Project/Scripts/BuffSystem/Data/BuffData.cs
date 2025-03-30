using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public class BuffData : ScriptableObject
    {
        public float duration;
        public StackBehavior stackBehavior;
        public int ticksPerSecond;
        
        public EffectInfo[] effects;
    }
}