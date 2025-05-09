using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "AttackType", menuName = "EffectSystem/Types/AttackType")]
    public class DamageType : EffectType
    {
        [SerializeField] private Color color = Color.white;
        [SerializeField] private StatType flatResistanceStat;
        [SerializeField] private StatType percentResistanceStat;

        public Color Color => color;

        public int ResistanceDamage(int damage, StatComponent statComponent)
        {
            if (flatResistanceStat) damage = statComponent.GetStat(flatResistanceStat).TransformNegative(damage);
            if (percentResistanceStat) damage = statComponent.GetStat(percentResistanceStat).TransformNegative(damage);
            return damage;
        }
    }
}