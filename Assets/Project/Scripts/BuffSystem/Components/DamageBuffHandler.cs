using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Attacks;

namespace Project.Scripts.BuffSystem.Components
{
    public class DamageBuffHandler : BuffHandler<IDamageable>
    {
        public DamageBuffHandler(BuffManager manager) : base(manager)
        {
        }

        public override void AddBuff(IBuff<IDamageable> buff)
        {
            CentralBuffTicker.Instance.RegisterBuff(buff);
        }

        public override void RemoveBuff(IBuff<IDamageable> buff)
        {
            CentralBuffTicker.Instance.UnregisterBuff(buff);
        }
    }
}