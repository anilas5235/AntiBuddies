using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    [CreateAssetMenu(fileName = "NewHeal", menuName = "EffectsSys/Heal")]
    public class HealData : BaseEffectData<IHeal>
    {
        [SerializeField] protected HealingType healingType;
        
        protected enum HealingType
        {
            NormalHeal,
            HealPercent,
        }
        
        public override IHeal GetEffect(GameObject source)
        {
            return healingType switch
            {
                HealingType.NormalHeal => new NormalHeal(source, amount),
                HealingType.HealPercent => new HealPercent(source, amount),
                _ => throw new System.NotImplementedException($"Healing type {healingType} is not implemented.")
            };
        }
    }
}