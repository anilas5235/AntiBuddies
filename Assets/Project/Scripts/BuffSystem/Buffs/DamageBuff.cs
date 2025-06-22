using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;

namespace Project.Scripts.BuffSystem.Buffs
{
    public class DamageBuff : Buff
    {
        private readonly DamagePackage _damagePackage;

        public DamageBuff(DamagePackage damagePackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick) : base(ConstructName(damagePackage.DamageType.Name, stack, tick),
            duration, hub, stack, tick, false)
        {
            _damagePackage = damagePackage;
        }

        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_damagePackage);
        }

        public override IBuff GetCopy()
        {
            return new DamageBuff(_damagePackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}