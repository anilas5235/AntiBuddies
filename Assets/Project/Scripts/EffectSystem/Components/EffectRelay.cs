using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.EffectSystem.Visuals;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Project.Scripts.EffectSystem.Components
{
    public class EffectRelay : MonoBehaviour, IPackageHub, INeedStatComponent
    {
        [SerializeField] protected AllyGroup allyGroup;

        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private BuffManager buffManager;
        [SerializeField] private EffectPipeline effectPipeline;
        [SerializeField] private StatRef dodgeStat;
        private StatComponent _statComponent;

        public event Action<int, DamageType> OnDamageReceived;
        public event Action<int, HealType> OnHealReceived;

        public UnityEvent onDamageReceived;

        public virtual void Apply(DamagePackage package)
        {
            if (dodgeStat.IsValid && dodgeStat.GetValue() >= Random.Range(1, 100))
            {
                if (effectPipeline)
                {
                    effectPipeline.Execute(this, EffectPipelineMode.SelfDodge);
                    IPackageHub other = package.Source.GetComponent<IPackageHub>();
                    if (other != null)
                    {
                        effectPipeline.Execute(other, EffectPipelineMode.OtherDodge);
                    }
                }

                return;
            }

            int damage = package.Amount;
            if (_statComponent) damage = package.ReceptionScale(damage, _statComponent);
            damage = healthComponent.TakeDamage(damage);
            OnDamageReceived?.Invoke(damage, package.DamageType);
            onDamageReceived?.Invoke();
            FloatingNumberSpawner.Instance.SpawnFloatingNumber(damage, GetDamageColor(package.DamageType), gameObject);
        }
        
        protected virtual Color GetDamageColor(DamageType damageType)
        {
            return damageType.Color;
        }

        public void Apply(HealPackage package)
        {
            int amount = package.Amount;
            amount = healthComponent.Heal(amount);
            OnHealReceived?.Invoke(amount, package.HealType);
        }

        public void Apply(StatPackage package)
        {
            _statComponent.ModifyStat(package);
        }

        public void OnStatInit(StatComponent statComponent)
        {
            _statComponent = statComponent;
            dodgeStat.Init(statComponent);
        }

        public bool IsAlie(AllyGroup group) => group == allyGroup;

        public void Apply(IBuff package)
        {
            buffManager?.AddBuff(package);
        }
    }
}