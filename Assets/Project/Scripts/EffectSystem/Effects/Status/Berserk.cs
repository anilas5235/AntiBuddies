using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Berserk : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "Berserk",
            "Increases damage dealt by the target by amount% but also increases damage taken by amount%.",
            Color.grey
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public Berserk(GameObject source, int amount)
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