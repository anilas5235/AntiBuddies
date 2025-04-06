using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public class Status : Effect<IStatusEffectable>
    {
        public Status(GameObject source, int amount) : base(source, amount)
        {
        }

        public override void Apply(IStatusEffectable target)
        {
            throw new System.NotImplementedException();
        }
    }
}