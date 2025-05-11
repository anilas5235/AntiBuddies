using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Buffs.ExitBehaviour;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    public abstract class BuffData<T> : ScriptableObject where T : EffectType
    {
        [SerializeField] private float duration;
        [SerializeField] private StackingBehavior stackBehavior;
        [SerializeField] private TickingBehavior tickBehavior;
        [SerializeField] private ExitBehavior exitBehavior;
        [SerializeField] private int ticksPerSecond;

        [SerializeField] private EffectDef<T> effect;
        private float TickInterval => 1f / ticksPerSecond;

        public IBuff GetBuff(IPackageTarget<T> target, GameObject source, StatComponent statComponent)
        {
            if (target == null) return null;
            EffectPackage<T> e = effect.CreatePackage(source, statComponent);
            return new Buff<T>(e, duration, target, GetStackBehavior(), GetTickBehavior(), GetExitBehavior());
        }

        #region Behaviours

        private IExitBehaviour GetExitBehavior()
        {
            return exitBehavior switch
            {
                ExitBehavior.None => null,
                ExitBehavior.Inverse => new InverseEffectBehaviour(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private ITickBehaviour GetTickBehavior()
        {
            return tickBehavior switch
            {
                TickingBehavior.None => null,
                TickingBehavior.Ticking => new Ticking(TickInterval),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IStackBehaviour GetStackBehavior()
        {
            return stackBehavior switch
            {
                StackingBehavior.None => new NotStacking(),
                StackingBehavior.Refresh => new Refreshing(),
                StackingBehavior.Stacking => new Stacking(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private enum ExitBehavior : byte
        {
            None,
            Inverse,
        }

        private enum TickingBehavior : byte
        {
            None,
            Ticking,
        }

        private enum StackingBehavior : byte
        {
            None,
            Refresh,
            Stacking,
        }

        #endregion
    }
}