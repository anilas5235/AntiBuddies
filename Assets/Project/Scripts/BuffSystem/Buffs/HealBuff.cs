using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class HealBuff : Buff
    {
        private readonly HealPackage _healPackage;

        public HealBuff(HealPackage healPackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick)
            : base(ConstructName(healPackage.HealType.Name, stack, tick), duration, hub, stack, tick, true)
        {
            _healPackage = healPackage;
        }

        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_healPackage);
        }

        public override IBuff GetCopy()
        {
            return new HealBuff(_healPackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}