using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Weak : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "Weak",
            "Reduces the target's attack power by a percentage",
            Color.grey
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public Weak(GameObject source, int amount)
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