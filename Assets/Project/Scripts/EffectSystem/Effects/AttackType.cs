using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class AttackType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private bool affectedByPercentModifier;
        [SerializeField] private bool affectedByFlatModifier;
        
        public Color Color => color;
        public bool AffectedByPercentModifier => affectedByPercentModifier;
        public bool AffectedByFlatModifier => affectedByFlatModifier;
    }
}