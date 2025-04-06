using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Status
{
    public abstract class Status : Effect<IStatusEffectable>
    {
        private readonly StatusType _statusType;
        protected Status(GameObject source, int amount, StatusType statusType) : base(source, amount)
        {
            _statusType = statusType;
        }
        
        public StatusType StatusType => _statusType;

        public override void Apply(IStatusEffectable target)
        {
            target.Apply(this);
        }
    }
}