using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class ArmorBreaking : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "ArmorBreaking",
            "Reduces the target's armor by a percentage"
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public ArmorBreaking(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }

        public void Apply(IStatusEffectable applyTarget)
        {
            throw new System.NotImplementedException();
        }
    }
}