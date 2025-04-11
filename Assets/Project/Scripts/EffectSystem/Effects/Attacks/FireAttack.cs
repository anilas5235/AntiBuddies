using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class FireAttack : IAttack
    {
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; } = "Fire Attack";
        public string Description { get; } = "Damages the Target with a Fire Type Damage";
        public Color Color { get; } = new(1f, 0.7f, 0.18f);

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