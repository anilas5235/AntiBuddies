using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Status;

namespace Project.Scripts.BuffSystem.Components
{
    public class StatBuffHandler : BuffHandler<IStatusEffectable>
    {
        public StatBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff<IStatusEffectable> buff)
        {
            CentralBuffTicker.Instance.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff<IStatusEffectable> buff)
        {
            CentralBuffTicker.Instance.UnregisterBuff(buff);
        }
    }
}