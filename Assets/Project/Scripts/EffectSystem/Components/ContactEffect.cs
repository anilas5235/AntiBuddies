using System;
using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactEffect : ContactTrigger
    {
        [Header("Effects")] public EffectCollection<DamageType> damageEffects = new();
        public EffectCollection<HealType> healEffects = new();
        public EffectCollection<StatType> statEffects = new();

        [Header("Buffs")] public BuffCollection<DamageType> damagingBuffs = new();
        public BuffCollection<HealType> healingBuffs = new();
        public BuffCollection<StatType> statBuffs = new();

        [Header("Settings")] public AlieGroup alieGroup;
        public StatComponent statComponent;
        [SerializeField] private bool applyOnlyOncePerObject = true;
        public event Action OnEffectApplied;

        private readonly List<GameObject> _contacts = new();

        protected override void HandleContact(GameObject other)
        {
            if (!other || other == gameObject) return;
            if (applyOnlyOncePerObject)
            {
                if (_contacts.Contains(other)) return;
                _contacts.Add(other);
            }

            int applies = 0;
            if (damageEffects.ApplyEffects(other, alieGroup, statComponent, gameObject)) applies++;
            if (healEffects.ApplyEffects(other, alieGroup, statComponent, gameObject)) applies++;
            if (statEffects.ApplyEffects(other, alieGroup, statComponent, gameObject)) applies++;
            if (damagingBuffs.ApplyBuffs(other, statComponent, gameObject)) applies++;
            if (healingBuffs.ApplyBuffs(other, statComponent, gameObject)) applies++;
            if (statBuffs.ApplyBuffs(other, statComponent, gameObject)) applies++;

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