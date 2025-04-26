using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class AttackType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private StatType flatModifier;
        [SerializeField] private StatType percentModifier;

        public Color Color => color;
        public bool AffectedByPercentModifier => flatModifier;
        public bool AffectedByFlatModifier => percentModifier;
        public StatType FlatModifier => flatModifier;
        public StatType PercentModifier => percentModifier;
    }
}