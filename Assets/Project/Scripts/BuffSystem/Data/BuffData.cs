using Project.Scripts.DamageSystem.Attacks;
using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "BuffSystem/BuffData")]
    public class BuffData : ScriptableObject
    {
        public float Duration;
        public StackBehavior StackBehavior;
        public TickBehavior TickBehavior;
        public int TicksPerSecond;
        
        public EffectInfo Effect;
        public float TickInterval => 1f / TicksPerSecond;
    }
}