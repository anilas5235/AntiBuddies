using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.BuffSystem.Buffs
{
    /// <summary>
    /// Buff that applies a heal package to the target.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class HealBuff : Buff
    {
        /// <summary>
        /// The heal package to apply.
        /// </summary>
        private readonly HealPackage _healPackage;

        /// <param name="healPackage">The heal package to apply.</param>
        /// <param name="duration">The duration of the buff.</param>
        /// <param name="hub">The hub for applying effects.</param>
        /// <param name="stack">The stack behaviour.</param>
        /// <param name="tick">The tick behaviour.</param>
        public HealBuff(HealPackage healPackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick)
            : base(healPackage.HealType.Name, duration, hub, stack, tick, true)
        {
            _healPackage = healPackage;
        }

        /// <inheritdoc/>
        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_healPackage);
        }

        /// <inheritdoc/>
        public override IBuff GetCopy()
        {
            return new HealBuff(_healPackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}