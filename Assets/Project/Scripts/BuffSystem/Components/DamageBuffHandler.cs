using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Attacks;

namespace Project.Scripts.BuffSystem.Components
{
    public class DamageBuffHandler : BuffHandler
    {
        public DamageBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.DamageBuffGroup.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff buff)
        {
            CentralBuffTicker.Instance.DamageBuffGroup.UnregisterBuff(buff);
        }
    }
}