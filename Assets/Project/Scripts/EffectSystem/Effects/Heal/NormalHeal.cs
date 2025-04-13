using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public class NormalHeal : IHeal
    {
        private static readonly EffectConstants Constants = new(
            "Heal",
            "Heals the target by a flat amount",
            new Color(0.18f, 1f, 0.18f)
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }
       
        public int CalculateHealing(HealingStats stats, IHealable healable)
        {
            return stats.healingAmplifier.TransformPositive(Amount);
        }

        public NormalHeal(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        public void Apply(IHealable target) => target.Apply(this);
    }
}