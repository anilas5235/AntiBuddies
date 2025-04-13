using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : IAttack
    {
        private static readonly EffectConstants Constants = new(
            "Fire",
            "Damages the Target with a Fire Type Damage",
            new Color(1f, 0.7f, 0.18f));

        public EffectConstants EffectConstants => Constants;
        public GameObject Source { get; }
        public int Amount { get; }

        public FireAttack(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }

        public int CalculateDamage(ResistanceComponent resistanceComponent)
            => IAttack.CalculateDamage(Amount, resistanceComponent.fireResistance);

        public void Apply(IDamageable applyTarget) => applyTarget.Apply(this);
    }
}