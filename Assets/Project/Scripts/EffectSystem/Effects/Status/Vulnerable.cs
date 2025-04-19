using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Vulnerable : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "Vulnerable",
            "Increases damage taken by the target.",
            Color.grey
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public Vulnerable(GameObject source, int amount)
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