using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Positive
{
    public class HealBurst : Effect<IHealable>
    {
        private readonly int _amount;
        
        public HealBurst(GameObject source, int amount) : base(source)
        {
            _amount = amount;
        }

        public override void Apply(IHealable target)
        {
            target?.Heal(_amount);
        }
    }
}