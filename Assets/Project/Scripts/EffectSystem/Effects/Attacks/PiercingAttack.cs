using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : IAttack
    {
        private static readonly EffectConstants Constants = new(
            "Piercing",
            "Damages the Target with a Piercing Type Damage"
        );

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public PiercingAttack(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        
        public void Apply(IDamageable applyTarget) => applyTarget.Apply(this);

        public int CalculateDamage(ResistanceComponent resistanceComponent)
            => IAttack.CalculateDamage(Amount, resistanceComponent.flatDamageReduction,
                resistanceComponent.piercingResistance);
    }
}