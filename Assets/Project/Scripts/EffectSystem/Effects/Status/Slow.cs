using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Slow : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "Slowed",
            "Reduces the target's speed by a percentage",
            Color.grey
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }
        
        public Slow(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        public void Apply(IStatusEffectable target)
        {
            throw new System.NotImplementedException();
        }
    }
}