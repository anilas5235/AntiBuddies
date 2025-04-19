using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Buffs.ExitBehaviour;
using Project.Scripts.BuffSystem.Buffs.StackBehaviour;
using Project.Scripts.BuffSystem.Buffs.TickBehaviour;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewBuffData", menuName = "BuffSystem/BuffData")]
    public class BuffData : ScriptableObject
    {
        [SerializeField] private float duration;
        [SerializeField] private StackingBehavior stackBehavior;
        [SerializeField] private TickingBehavior tickBehavior;
        [SerializeField] private ExitBehavior exitBehavior;
        [SerializeField] private int ticksPerSecond;

        [SerializeField] private EffectData effect;
        private float TickInterval => 1f / ticksPerSecond;

        public IBuff GetBuff(ITarget<EffectPackage> target, GameObject source, AlieGroup alieGroup)
        {
            EffectPackage e = effect.GetPackage(source, alieGroup);
            return new Buff(e, duration,  target, GetStackBehavior(), GetTickBehavior(), GetExitBehavior());
        }

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
    }
}