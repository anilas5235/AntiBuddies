using System;
using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Data.Type;
using Project.Scripts.EffectSystem.Effects.ExtraEffects;
using Project.Scripts.EffectSystem.Visuals;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Handles the application and relay of effects, such as damage, healing, stats, and buffs.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class EffectRelay : MonoBehaviour, IPackageHub, INeedStatGroup
    {
        /// <summary>
        /// The allied group this entity belongs to.
        /// </summary>
        [SerializeField] protected AlliedGroup alliedGroup;

        /// <summary>
        /// Reference to the health component.
        /// </summary>
        [SerializeField] private HealthComponent healthComponent;

        /// <summary>
        /// Reference to the buff manager.
        /// </summary>
        [SerializeField] private BuffManager buffManager;

        /// <summary>
        /// Reference to the extra effect handler.
        /// </summary>
        [SerializeField] private ExtraEffectHandler extraEffectHandler;

        /// <summary>
        /// Reference to the dodge stat.
        /// </summary>
        [SerializeField] private StatRef dodgeStat;

        /// <summary>
        /// The stat group associated with this relay.
        /// </summary>
        private IStatGroup _statGroup;

        /// <summary>
        /// Event triggered when damage is received.
        /// </summary>
        public event Action<int, DamageType> OnDamageReceived;

        /// <summary>
        /// Event triggered when healing is received.
        /// </summary>
        public event Action<int, HealType> OnHealReceived;

        /// <summary>
        /// Unity event triggered when damage is received.
        /// </summary>
        public UnityEvent onDamageReceived;

        /// <summary>
        /// Applies a damage package to this entity.
        /// </summary>
        /// <param name="package">The damage package.</param>
        public virtual void Apply(DamagePackage package)
        {
            int damage = package.Amount;
            // Scale damage based on stat group if available.
            if (_statGroup != null) damage = package.ReceptionScale(damage, _statGroup);
            if (damage <= 0) return;

            // Dodge logic: If dodge stat is valid and succeeds, trigger dodge effects and return.
            if (dodgeStat.IsValid && dodgeStat.GetValue() >= Random.Range(1, 100))
            {
                if (extraEffectHandler)
                {
                    extraEffectHandler.Execute(this, ExtraEffectHandler.TriggerType.SelfDodge);
                }

                FloatingTextSpawner.Instance.SpawnFloatingText("DODGE", Color.white, gameObject);
                return;
            }

            // Apply damage to the health component.
            damage = healthComponent.TakeDamage(damage);

            // Trigger Event and effects for damage taken.
            extraEffectHandler?.Execute(this, ExtraEffectHandler.TriggerType.TakeDamage);
            OnDamageReceived?.Invoke(damage, package.DamageType);
            onDamageReceived?.Invoke();
            FloatingTextSpawner.Instance.SpawnFloatingNumber(damage, GetDamageColor(package), gameObject);
        }

        /// <summary>
        /// Applies a healing package to this entity.
        /// </summary>
        /// <param name="package">The heal package.</param>
        public void Apply(HealPackage package)
        {
            int amount = package.Amount;
            amount = healthComponent.Heal(amount);
            OnHealReceived?.Invoke(amount, package.HealType);
            FloatingTextSpawner.Instance.SpawnFloatingNumber(amount, package.HealType.Color, gameObject);
        }

        /// <summary>
        /// Applies a stat modification package to this entity.
        /// </summary>
        /// <param name="package">The stat package.</param>
        public void Apply(StatPackage package)
        {
            _statGroup.ModifyStat(package);
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
            dodgeStat.OnStatInit(statGroup);
        }

        /// <inheritdoc/>
        public bool IsAlie(AlliedGroup group) => group == alliedGroup;

        /// <summary>
        /// Applies a buff to this entity.
        /// </summary>
        /// <param name="package">The buff to apply.</param>
        public void Apply(IBuff package)
        {
            buffManager?.AddBuff(package);
        }

        /// <summary>
        /// Gets the color associated with the damage.
        /// </summary>
        /// <param name="damagePackage">The damage package from which to get the color.</param>
        /// <returns>The color representing the damage.</returns>
        protected virtual Color GetDamageColor(DamagePackage damagePackage)
        {
            return damagePackage.GetDamageColor();
        }
    }
}