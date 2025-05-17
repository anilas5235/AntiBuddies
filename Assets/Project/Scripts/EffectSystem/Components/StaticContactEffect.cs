using System;
using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class StaticContactEffect : ContactTrigger
    {
        [Header("Effects")] public EffectPackageCollection<DamageType> damageEffects = new();
        public EffectPackageCollection<HealType> healEffects = new();
        public EffectPackageCollection<StatType> statEffects = new();

        [Header("Buffs")] public BuffDataCollection<DamageType> damagingBuffs = new();
        public BuffDataCollection<HealType> healingBuffs = new();
        public BuffDataCollection<StatType> statBuffs = new();

        [Header("Settings")] public AlieGroup alieGroup;
        [SerializeField] private bool applyOnlyOncePerObject = true;
        [SerializeField] private bool applyToSource;

        private GameObject _source;
        public event Action OnEffectApplied;

        private readonly List<GameObject> _contacts = new();

        public void SetSource(GameObject source) => _source = source;

        private List<IApplyEffect> _applyEffects = new();
        private List<IApplyDynamicEffect> _dynamicEffects = new();

        private void Awake()
        {
            _applyEffects = new List<IApplyEffect>()
            {
                damageEffects,
                healEffects,
                statEffects,
            };
            _dynamicEffects = new List<IApplyDynamicEffect>()
            {
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
            if (!applyToSource && other == _source) return;
            if (applyOnlyOncePerObject)
            {
                if (_contacts.Contains(other)) return;
                _contacts.Add(other);
            }

            int applies = 0;
            foreach (IApplyEffect effect in _applyEffects)
            {
                applies += effect.Apply(other, alieGroup);
            }
            foreach (IApplyDynamicEffect effect in _dynamicEffects)
            {
                applies += effect.Apply(other, alieGroup, null, _source);
            }

            if (applies > 0) OnEffectApplied?.Invoke();
        }

        public void ClearAll()
        {
            damageEffects.Clear();
            healEffects.Clear();
            statEffects.Clear();
            damagingBuffs.Clear();
            healingBuffs.Clear();
            statBuffs.Clear();
            ClearContacts();
        }

        public void ClearContacts() => _contacts.Clear();
    }
}