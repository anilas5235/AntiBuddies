using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class StatBuff : Buff
    {
        private readonly StatPackage _statPackage;
        private readonly StatPackage _statPackageInverse;
        private int _numberOfApplies;

        public StatBuff(StatPackage statPackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick, bool affectsAlly = false)
            : base(ConstructName(statPackage.StatType.Name, stack, tick), duration, hub, stack, tick, affectsAlly)
        {
            _statPackage = statPackage;
            _statPackageInverse = statPackage.Inverse();
        }

        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_statPackage);
            _numberOfApplies++;
        }

        public override void OnBuffRemove()
        {
            base.OnBuffRemove();
            for (int i = 0; i < _numberOfApplies; i++)
            {
                Hub?.Apply(_statPackageInverse);
            }
        }

        public override IBuff GetCopy()
        {
            return new StatBuff(_statPackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}