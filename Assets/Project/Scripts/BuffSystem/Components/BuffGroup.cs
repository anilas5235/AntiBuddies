using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.BuffSystem.Components
{
    [Serializable]
    public class BuffGroup
    {
        private const int MaxBuffCount = 1000;
        [SerializeField] private float lastTickTime;
        [SerializeField] private int buffCount;
        private event Action<float> OnBuffTick;
        public int BuffCount => buffCount;

        public bool HasSpace => buffCount < MaxBuffCount;
        public bool IsFull => !HasSpace;
        
        public bool RegisterBuff([NotNull] IBuff buff)
        {
            if(IsFull) return false;
            OnBuffTick += buff.OnBuffTick;
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
            float deltaTime = now - lastTickTime;
            lastTickTime = now;
            OnBuffTick?.Invoke(deltaTime);
        }
    }
}