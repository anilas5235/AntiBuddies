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
        
        /// <summary>
        /// Represents a group of buffs that will be ticked together
        /// </summary>
        public class BuffGroup
        {
            public string Name { get; }
            public event Action<float> OnBuffTick;
            
            public BuffGroup(string name)
            {
                Name = name;
            }
            
            public void RegisterBuff([NotNull] IBuff buff)
            {
                OnBuffTick += buff.OnBuffTick;
            }
            
            public void UnregisterBuff([NotNull] IBuff buff)
            {
                OnBuffTick -= buff.OnBuffTick;
            }
            
            public void Tick(float deltaTime)
            {
                OnBuffTick?.Invoke(deltaTime);
            }
        }
        
        // Predefined buff groups
        private readonly List<BuffGroup> _buffGroups = new();
        
        // Common buff groups that can be accessed directly
        public BuffGroup DamageBuffs { get; private set; }
        public BuffGroup HealBuffs { get; private set; }
        public BuffGroup StatusBuffs { get; private set; }

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

        public void RegisterBuffToDamageGroup([NotNull] IBuff buff) => DamageBuffs.RegisterBuff(buff);
        public void RegisterBuffToHealGroup([NotNull] IBuff buff) => HealBuffs.RegisterBuff(buff);
        public void RegisterBuffToStatusGroup([NotNull] IBuff buff) => StatusBuffs.RegisterBuff(buff);
        
        public void UnregisterBuffFromDamageGroup([NotNull] IBuff buff) => DamageBuffs.UnregisterBuff(buff);
        public void UnregisterBuffFromHealGroup([NotNull] IBuff buff) => HealBuffs.UnregisterBuff(buff);
        public void UnregisterBuffFromStatusGroup([NotNull] IBuff buff) => StatusBuffs.UnregisterBuff(buff);

        // For backward compatibility
        public void RegisterBuff([NotNull] IBuff buff) => DamageBuffs.RegisterBuff(buff);
        public void UnregisterBuff([NotNull] IBuff buff) => DamageBuffs.UnregisterBuff(buff);
        
        // Create custom buff groups
        public BuffGroup CreateBuffGroup(string name)
        {
            var group = new BuffGroup(name);
            _buffGroups.Add(group);
            return group;
        }

        private void Awake()
        {
            if (_instance && _instance != this) Destroy(gameObject);
            else
            {
                _instance = this;
                InitializeBuffGroups();
            }
        }
        
        private void InitializeBuffGroups()
        {
            // Initialize the predefined buff groups
            DamageBuffs = new BuffGroup("Damage");
            HealBuffs = new BuffGroup("Heal");
            StatusBuffs = new BuffGroup("Status");
            
            // Add them to the list
            _buffGroups.Add(DamageBuffs);
            _buffGroups.Add(HealBuffs);
            _buffGroups.Add(StatusBuffs);
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
            _coroutine ??= StartCoroutine(Tick(TickInterval));
        }

        private void StopTicking()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator Tick(float tickInterval)
        {
            WaitForSeconds wait = new(tickInterval / _buffGroups.Count);
            float lastTickTime = Time.time;
            
            while (true)
            {
                foreach (BuffGroup buffGroup in _buffGroups)
                {
                    // Calculate the time since the last tick
                    float now = Time.time;
                    float deltaTime = now - lastTickTime;
                    lastTickTime = now;

                    // Tick this buff group
                    buffGroup.Tick(deltaTime);
                    
                    // Wait before processing the next group
                    yield return wait;
                }
            }
        }
    }
}
