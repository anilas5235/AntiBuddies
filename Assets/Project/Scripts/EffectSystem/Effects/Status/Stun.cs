using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Stun : IStatus
    {
        private static readonly EffectConstants Constants = new(
            "Stun",
            "Stops the target from acting for a duration",
            Color.yellow
        );
        public GameObject Source { get; }
        public int Amount { get; }
        public EffectConstants EffectConstants => Constants;

        public Stun(GameObject source, int amount)
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