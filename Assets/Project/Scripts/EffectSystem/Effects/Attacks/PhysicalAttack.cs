using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PhysicalAttack : IAttack
    {
        private static readonly EffectConstants Constants = new(
            "Physical",
            "Damages the Target with a Physical Type Damage"
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public PhysicalAttack(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }

        public void Apply(IDamageable applyTarget) => applyTarget.Apply(this);

        public int CalculateDamage(ResistanceComponent resistanceComponent)
        {
            if (!resistanceComponent) return Amount;
            return IAttack.CalculateDamage(Amount, resistanceComponent.flatDamageReduction,
                resistanceComponent.physicalResistance);
        }
    }
}