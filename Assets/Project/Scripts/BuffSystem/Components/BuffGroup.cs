using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Project.Scripts.BuffSystem.Buffs;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// Represents a group of buffs that will be ticked together
    /// </summary>
    public class BuffGroup : MonoBehaviour
    {
        private const int IntervalInFixedUpdates = 10;
        public string Name { get; set; }
        public event Action<float> OnBuffTick;

        private Coroutine _coroutine;

        private CentralBuffTicker CentralBuffTicker => CentralBuffTicker.Instance;

        private void OnEnable() => _coroutine = StartCoroutine(Ticking());

        private void OnDisable() => StopCoroutine(_coroutine);

        public BuffGroup RegisterBuff([NotNull] IBuff buff)
        {
            OnBuffTick += buff.OnBuffTick;
            return this;
        }

        public void UnregisterBuff([NotNull] IBuff buff)
        {
            OnBuffTick -= buff.OnBuffTick;
        }

        private void Tick(float deltaTime)
        {
            OnBuffTick?.Invoke(deltaTime);
        }

        private void OnDestroy() => StopCoroutine(_coroutine);

        private IEnumerator Ticking()
        {
            float lastTickTime = Time.time;
            WaitForFixedUpdate wait = new();
            while (true)
            {
                // Calculate the time since the last tick
                float now = Time.time;
                float deltaTime = now - lastTickTime;
                lastTickTime = now;

                // Invoke the tick event with the delta time
                Tick(deltaTime);
                
                // Wait for the next tick
                for (int i = 0; i < IntervalInFixedUpdates; i++)
                {
                    yield return wait;
                }
            }
        }
    }
}