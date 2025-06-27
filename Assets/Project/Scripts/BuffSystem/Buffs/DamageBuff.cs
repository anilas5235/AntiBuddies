using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.BuffSystem.Buffs
{
    /// <summary>
    /// Buff that applies a damage package to the target.
    /// </summary>
    public class DamageBuff : Buff
    {
        /// <summary>
        /// The damage package to apply.
        /// </summary>
        private readonly DamagePackage _damagePackage;

        /// <param name="damagePackage">The damage package to apply.</param>
        /// <param name="duration">The duration of the buff.</param>
        /// <param name="hub">The hub for applying effects.</param>
        /// <param name="stack">The stack behaviour.</param>
        /// <param name="tick">The tick behaviour.</param>
        public DamageBuff(DamagePackage damagePackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick)
            : base(damagePackage.DamageType.Name, duration, hub, stack, tick, false)
        {
            _damagePackage = damagePackage;
        }

        /// <inheritdoc/>
        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_damagePackage);
        }

        /// <inheritdoc/>
        public override IBuff GetCopy()
        {
            return new DamageBuff(_damagePackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}