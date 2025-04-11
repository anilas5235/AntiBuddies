using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PhysicalAttack : IAttack
    {
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; } = "Physical Attack";
        public string Description { get; } = "Damages the Target with a Physical Type Damage";
        public Color Color { get; } = Color.white;

        public PhysicalAttack(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }
        
        public void Apply(IDamageable applyTarget) => applyTarget.Apply(this);

        public int CalculateDamage(ResistanceComponent resistanceComponent)
            => IAttack.CalculateDamage(Amount, resistanceComponent.flatDamageReduction,
                resistanceComponent.physicalResistance);
    }
}