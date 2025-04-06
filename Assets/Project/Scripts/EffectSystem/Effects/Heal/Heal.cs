using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Heal
{
    public abstract class Heal : Effect<IHealable>
    {
        private readonly HealingType _healingType;

        protected Heal(GameObject source, int amount, HealingType healingType) : base(source,amount)
        {
            _healingType = healingType;
        }

        public override void Apply(IHealable target)
        {
            target?.Heal(CalculateHealing(target));
        }
        
        protected abstract int CalculateHealing(IHealable target);
    }
}