using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class CentralBuffTicker : MonoBehaviour
    {
        // This class is responsible for managing the central buff ticker in the game.
        // The class will be a singleton to ensure only one instance exists in the scene.

        private static CentralBuffTicker _instance;
        private const float TickInterval = 0.1f;

        public static event Action<float> OnDamageBuffTick;
        public static event Action<float> OnHealBuffTick;
        public static event Action<float> OnStatusBuffTick;

        public static CentralBuffTicker Instance
        {
            get
            {
                if (_instance) return _instance;
                // Try to find an existing instance in the scene
                _instance = FindFirstObjectByType<CentralBuffTicker>();
                if (_instance) return _instance;
                // If no instance is found, create a new one
                GameObject obj = new("CentralBuffTicker");
                _instance = obj.AddComponent<CentralBuffTicker>();
                return _instance;
            }
        }

        private Coroutine _coroutine;
        private List<Action<float>> _tickEvents = new() { OnDamageBuffTick, OnHealBuffTick, OnStatusBuffTick };

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