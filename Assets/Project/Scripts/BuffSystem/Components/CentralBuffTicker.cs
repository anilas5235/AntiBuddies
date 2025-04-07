using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Attacks;
using Project.Scripts.EffectSystem.Effects.Heal;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    /// <summary>
    /// CentralBuffTicker is a singleton class that manages the central buff ticker in the game.
    /// </summary>
    public class CentralBuffTicker : MonoBehaviour
    {
        private static CentralBuffTicker _instance;
        private const float TickInterval = 0.1f;
        private static event Action<float> OnDamageBuffTick;
        private static event Action<float> OnHealBuffTick;
        private static event Action<float> OnStatusBuffTick;

        public static CentralBuffTicker Instance
        {
            get
            {
                if (_instance) return _instance;
                _instance = FindFirstObjectByType<CentralBuffTicker>(); // Try to find an existing instance in the scene
                if (_instance) return _instance;
                GameObject obj = new("CentralBuffTicker"); // If no instance is found, create a new one
                _instance = obj.AddComponent<CentralBuffTicker>();
                return _instance;
            }
        }

        private Coroutine _coroutine;
        private readonly List<Action<float>> _tickEvents = new() { OnDamageBuffTick, OnHealBuffTick, OnStatusBuffTick };

        public void RegisterBuff([NotNull] IBuff<IDamageable> buff) => OnDamageBuffTick += buff.OnBuffTick;
        public void UnregisterBuff([NotNull] IBuff<IDamageable> buff) => OnDamageBuffTick -= buff.OnBuffTick;
        public void RegisterBuff([NotNull] IBuff<IHealable> buff) => OnHealBuffTick += buff.OnBuffTick;
        public void UnregisterBuff([NotNull] IBuff<IHealable> buff) => OnHealBuffTick -= buff.OnBuffTick;
        public void RegisterBuff([NotNull] IBuff<IStatusEffectable> buff) => OnStatusBuffTick += buff.OnBuffTick;
        public void UnregisterBuff([NotNull] IBuff<IStatusEffectable> buff) => OnStatusBuffTick -= buff.OnBuffTick;


        private void Awake()
        {
            if (_instance && _instance != this) Destroy(gameObject);
            else _instance = this;
        }

        private void OnEnable()
        {
            StartTicking();
        }

        private void OnDisable()
        {
            StopTicking();
        }

        private void OnDestroy()
        {
            StopTicking();
            if (_instance == this) _instance = null;
        }

        private void StartTicking()
        {
            _coroutine ??= StartCoroutine(Tick(_tickEvents, TickInterval));
        }

        private void StopTicking()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private static IEnumerator Tick(List<Action<float>> tickEvents, float tickInterval)
        {
            WaitForSeconds wait = new(tickInterval / tickEvents.Count);
            float lastTickTime = Time.time;
            while (true)
            {
                foreach (Action<float> tickEvent in tickEvents)
                {
                    // Calculate the time since the last tick
                    float now = Time.time;
                    float deltaTime = now - lastTickTime;
                    lastTickTime = now;

                    // Invoke the tick event with the delta time
                    tickEvent?.Invoke(deltaTime);
                    yield return wait;
                }
            }
        }
    }
}