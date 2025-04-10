using Project.Scripts.EffectSystem.Components;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Attacks
{
    public class PiercingAttack : IAttack
    {
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; } = "Piercing Attack";
        public string Description { get; } = "Damages the Target with a Piercing Type Damage";
        public Color Color { get; } = new Color(0.83f, 0.48f, 1f);
        
        public PiercingAttack(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
        }

        public void Apply(IDamageable target)
            => target.ApplyDamage(this);

        public int CalculateDamage(ResistanceComponent resistanceComponent)
            => IAttack.CalculateDamage(Amount, resistanceComponent.flatDamageReduction,
                resistanceComponent.piercingResistance);
    }
}