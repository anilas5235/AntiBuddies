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
    public class EffectRelay : MonoBehaviour, IPackageHub, INeedStatGroup
    {
        [SerializeField] protected AllyGroup allyGroup;

        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private BuffManager buffManager;
        [SerializeField] private EffectPipeline effectPipeline;
        [SerializeField] private StatRef dodgeStat;
        private IStatGroup _statGroup;

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
            if (_statGroup != null) damage = package.ReceptionScale(damage, _statGroup);
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
            _statGroup.ModifyStat(package);
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
            dodgeStat.OnStatInit(statGroup);
        }

        public bool IsAlie(AllyGroup group) => group == allyGroup;

        public void Apply(IBuff package)
        {
            buffManager?.AddBuff(package);
        }
    }
}