using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;

namespace Project.Scripts.BuffSystem.Buffs
{
    /// <summary>
    /// Buff that applies a stat package to the target and removes it on expiration.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class StatBuff : Buff
    {
        /// <summary>
        /// The stat package to apply.
        /// </summary>
        private readonly StatPackage _statPackage;

        /// <summary>
        /// The inverse stat package to remove the effect.
        /// </summary>
        private readonly StatPackage _statPackageInverse;

        /// <summary>
        /// Tracks how many times the buff has been applied.
        /// </summary>
        private int _numberOfApplies;

        /// <param name="statPackage">The stat package to apply.</param>
        /// <param name="duration">The duration of the buff.</param>
        /// <param name="hub">The hub for applying effects.</param>
        /// <param name="stack">The stack behaviour.</param>
        /// <param name="tick">The tick behaviour.</param>
        /// <param name="affectsAlly">Whether the buff affects ally.</param>
        public StatBuff(StatPackage statPackage, float duration, IPackageHub hub, IStackBehaviour stack,
            ITickBehaviour tick, bool affectsAlly = false)
            : base(statPackage.StatType.Name, duration, hub, stack, tick, affectsAlly)
        {
            _statPackage = statPackage;
            _statPackageInverse = statPackage.Inverse();
        }

        /// <inheritdoc/>
        public override void OnBuffApply()
        {
            base.OnBuffApply();
            Hub?.Apply(_statPackage);
            _numberOfApplies++;
        }

        /// <inheritdoc/>
        public override void OnBuffRemove()
        {
            base.OnBuffRemove();
            // Remove the stat effect as many times as it was applied.
            Hub?.Apply(_statPackageInverse.Multiply(_numberOfApplies));
        }

        /// <inheritdoc/>
        public override IBuff GetCopy()
        {
            return new StatBuff(_statPackage, Duration, Hub, StackBehaviour, TickBehavior);
        }
    }
}