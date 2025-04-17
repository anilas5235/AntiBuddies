using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public class HealPercent : IHeal
    {
        private static readonly EffectConstants Constants = new(
            "HealPercent",
            "Heals the target for a percentage of their max health",
            new Color(0.18f, 1f, 0.18f)
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; }= "HealPercent";
        public string Description { get; } = "Heals the target for a percentage of their max health.";
        public Color Color { get; }
        
        public HealPercent(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }

        public int CalculateHealing(HealingStats stats, IHealable healable)
        {
            return Mathf.FloorToInt(Amount/100f * healable.MaxHealth);
        }
     
        public void Apply(IHealable applyTarget) => applyTarget.Apply(this);
    }
}