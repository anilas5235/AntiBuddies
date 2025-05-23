using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.EffectSystem.Visuals;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, IPackageHub, INeedStatComponent
    {
        [SerializeField] private AlieGroup alieGroup;
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private BuffManager buffManager;
        private StatComponent _statComponent;
        
        public event Action<int, DamageType, GameObject> OnDamageReceived;
        public event Action<int, HealType, GameObject> OnHealReceived;

        private void OnEnable()
        {
            OnDamageReceived += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealReceived += FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }
        
        private void OnDisable()
        {
            OnDamageReceived -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
            OnHealReceived -= FloatingNumberSpawner.Instance.SpawnFloatingNumber;
        }

        public void Apply(DamagePackage package)
        {
            int damage = package.Amount;
            if (_statComponent) damage = package.ReceptionScale(damage, _statComponent);
            damage = healthComponent.TakeDamage(damage);
            OnDamageReceived?.Invoke(damage, package.DamageType, gameObject);
        }

        public void Apply(HealPackage package)
        {
            int amount = package.Amount;
            amount = healthComponent.Heal(amount);
            OnHealReceived?.Invoke(amount, package.HealType, gameObject);
        }

        public void Apply(StatPackage package)
        {
            _statComponent.ModifyStat(package);
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
        }

        public bool IsAlie(AlieGroup group) => group == alieGroup;
        public void Apply(IBuff package)
        {
            buffManager?.AddBuff(package);
        }
    }
}