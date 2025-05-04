using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "StatType", menuName = "EffectSystem/Types/StatType")]
    public class StatType : EffectType
    {
        [SerializeField] private bool isPercentage;
        public bool IsPercentage => isPercentage;
        
        [SerializeField] private int maxValue = int.MaxValue;
        [SerializeField] private int minValue = int.MinValue;
        public int MaxValue => maxValue;
        public int MinValue => minValue;
    }
}