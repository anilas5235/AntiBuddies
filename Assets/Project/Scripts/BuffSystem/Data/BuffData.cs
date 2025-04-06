using Project.Scripts.EffectSystem.Effects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "BuffSystem/BuffData")]
    public class BuffData : ScriptableObject
    {
        public float Duration;
        public StackBehavior StackBehavior;
        public TickBehavior TickBehavior;
        public int TicksPerSecond;
        
        [FormerlySerializedAs("Effect")] public AttackInfo attack;
        public float TickInterval => 1f / TicksPerSecond;
    }
}