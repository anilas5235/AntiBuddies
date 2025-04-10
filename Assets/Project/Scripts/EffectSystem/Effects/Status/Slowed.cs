using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Slowed : IStatus
    {
        public GameObject Source { get; }
        public int Amount { get; }
        public string Name { get; } = "Slowed";
        public string Description { get; } = "Reduces the target's speed by a percentage.";
        public Color Color { get; } = Color.grey;
        public StatusType StatusType { get; } 
        
        public Slowed(GameObject source, int amount)
        {
            Source = source;
            Amount = amount;
            StatusType = StatusType.Slow;
        }
        public void Apply(IStatusEffectable target)
        {
            throw new System.NotImplementedException();
        }

    }
}