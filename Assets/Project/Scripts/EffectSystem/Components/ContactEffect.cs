using System;
using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactEffect : ContactTrigger
    {
        [FormerlySerializedAs("damageEffects")] [Header("Effects")] public DynamicEffectCollection<DamageType> damageDynamicEffects = new();
        [FormerlySerializedAs("healEffects")] public DynamicEffectCollection<HealType> healDynamicEffects = new();
        [FormerlySerializedAs("statEffects")] public DynamicEffectCollection<StatType> statDynamicEffects = new();

        [Header("Buffs")] public BuffDataCollection<DamageType> damagingBuffs = new();
        public BuffDataCollection<HealType> healingBuffs = new();
        public BuffDataCollection<StatType> statBuffs = new();

        [Header("Settings")] public AlieGroup alieGroup;
        public StatComponent statComponent;
        [SerializeField] private bool applyOnlyOncePerObject = true;
        public event Action OnEffectApplied;

        private readonly List<GameObject> _contacts = new();
        
        private List<IApplyDynamicEffect> _applyEffects = new();

        private void Awake()
        {
            _applyEffects = new List<IApplyDynamicEffect>()
            {
                damageDynamicEffects,
                healDynamicEffects,
                statDynamicEffects,
                damagingBuffs,
                healingBuffs,
                statBuffs
            };
        }
        
        private void OnDisable()
        {
            ClearContacts();
        }

        protected override void HandleContact(GameObject other)
        {
            if (!other || other == gameObject) return;
            if (applyOnlyOncePerObject)
            {
                if (_contacts.Contains(other)) return;
                _contacts.Add(other);
            }

            int applies = 0;
            foreach (IApplyDynamicEffect effect in _applyEffects)
            {
                applies += effect.Apply(other, alieGroup, statComponent, gameObject);
            }

            if (applies > 0) OnEffectApplied?.Invoke();
        }

        public void ClearAll()
        {
            damageDynamicEffects.Clear();
            healDynamicEffects.Clear();
            statDynamicEffects.Clear();
            damagingBuffs.Clear();
            healingBuffs.Clear();
            statBuffs.Clear();
            ClearContacts();
        }

        public void ClearContacts() => _contacts.Clear();
    }
}