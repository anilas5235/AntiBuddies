using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public class NormalHeal : IHeal
    {
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; } = "NormalHeal Burst";
        public string Description { get; }= "Heals the Target with a Burst Type Healing";
        public Color Color { get; } = Color.green;
        
        public NormalHeal(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        public void Apply(IHealable target) => target.Apply(this);

        public int CalculateHealing(HealingStats stats)
        {
            throw new System.NotImplementedException();
        }
    }
}