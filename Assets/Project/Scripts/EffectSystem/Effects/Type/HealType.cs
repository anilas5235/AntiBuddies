using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Type
{
    [CreateAssetMenu(fileName = "HealType", menuName = "EffectSystem/Types/HealType")]
    public class HealType : EffectType
    {
        [SerializeField] private Color color = Color.green;
        [SerializeField] private StatType percentScaleStat;
        public Color Color => color;

        public override int ReceptionScale(int amount, StatComponent statComponent)
        {
            if (percentScaleStat)
            {
                amount = statComponent.GetStat(percentScaleStat)?.TransformPositive(amount) ?? amount;
            }
            return amount;
        }
    }
}
