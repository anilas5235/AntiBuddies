using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public class HealBurst : Heal
    {
        public HealBurst(GameObject source, int amount) 
            : base(source, amount, HealingType.HealBurst)
        {
        }

        protected override int CalculateHealing(IHealable target) => GetAmount();
    }
}