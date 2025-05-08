using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class AttackType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private StatType flatResistanceStat;
        [SerializeField] private StatType percentResistanceStat;

        public Color Color => color;
        public bool AffectedByPercentResistance => percentResistanceStat;
        public bool AffectedByFlatResistanceStat => flatResistanceStat;
        public StatType FlatResistanceStat => flatResistanceStat;
        public StatType PercentResistanceStat => percentResistanceStat;
    }
}