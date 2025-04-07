using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Heal;

namespace Project.Scripts.BuffSystem.Components
{
    public class HealBuffHandler : BuffHandler<IHealable>
    {
        public HealBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff<IHealable> buff)
        {
            CentralBuffTicker.Instance.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff<IHealable> buff)
        {
            CentralBuffTicker.Instance.UnregisterBuff(buff);
        }
    }
}