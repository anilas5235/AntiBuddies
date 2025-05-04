using System;
using System.Diagnostics.CodeAnalysis;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    [Serializable]
    public class BuffGroup
    {
        private const int MaxBuffCount = 1000;
        [SerializeField] private float tickDelta;
        [SerializeField] private int buffCount;
        [SerializeField] private float lastTickTime;
        private event Action<float> OnBuffTick;
        public int BuffCount => buffCount;

        public bool HasSpace => buffCount < MaxBuffCount;
        public bool IsFull => !HasSpace;
        
        public bool RegisterBuff([NotNull] IBuff buff)
        {
            if(IsFull) return false;
            OnBuffTick += buff.OnBuffTick;
            buff.BuffGroup = this;
            buffCount++;
            return true;
        }

        public void UnregisterBuff([NotNull] IBuff buff)
        {
            OnBuffTick -= buff.OnBuffTick;
            buffCount--;
        }

        internal void Tick()
        {
            float now = Time.time;
            tickDelta = now - lastTickTime;
            lastTickTime = now;
            OnBuffTick?.Invoke(tickDelta);
        }
    }
}