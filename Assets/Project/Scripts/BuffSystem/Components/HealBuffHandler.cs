using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Heal;

namespace Project.Scripts.BuffSystem.Components
{
    public class HealBuffHandler : BuffHandler
    {
        public HealBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.HealBuffGroup.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.HealBuffGroup.UnregisterBuff(buff);
        }
    }
}