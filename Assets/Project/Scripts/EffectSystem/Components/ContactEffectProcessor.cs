using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.ExtraEffects;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    /// <summary>
    /// Processes and applies effects, buffs, and stat changes to a contacted GameObject
    /// by interacting with its IPackageHub and handling ally/enemy logic.
    /// </summary>
    public class ContactEffectProcessor
    {
        /// <summary>
        /// The contacted GameObject.
        /// </summary>
        private GameObject ContactObject { get; }

        /// <summary>
        /// Optional handler for executing extra effects on triggers.
        /// </summary>
        private ExtraEffectHandler ExtraEffectHandler { get; }

        /// <summary>
        /// The IPackageHub component on the contacted GameObject.
        /// </summary>
        private IPackageHub Hub { get; }

        /// <summary>
        /// True if the contacted object is an ally.
        /// </summary>
        public bool Alie { get; }

        /// <summary>
        /// True if the processor is valid and can apply effects.
        /// </summary>
        public bool IsValid { get; } = true;

        /// <param name="contactObject">The contacted GameObject.</param>
        /// <param name="alliedGroup">The allied group of the source.</param>
        /// <param name="extraEffectHandler">Optional extra effect handler.</param>
        public ContactEffectProcessor(GameObject contactObject, AlliedGroup alliedGroup,
            ExtraEffectHandler extraEffectHandler)
        {
            ExtraEffectHandler = extraEffectHandler;
            ContactObject = contactObject;
            if (!ContactObject)
            {
                IsValid = false;
                return;
            }

            Hub = contactObject.GetComponent<IPackageHub>();
            if (Hub == null)
            {
                IsValid = false;
                return;
            }

            Alie = Hub.IsAlie(alliedGroup);
        }

        /// <summary>
        /// Applies a damage package to the contacted object if valid and not an ally.
        /// </summary>
        /// <param name="package">The damage package to apply.</param>
        public void Apply(DamagePackage package)
        {
            if (!IsValid || package == null || Alie) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, ExtraEffectHandler.TriggerType.Damage);
        }

        /// <summary>
        /// Applies a heal package to the contacted object if valid and is an ally.
        /// </summary>
        /// <param name="package">The heal package to apply.</param>
        public void Apply(HealPackage package)
        {
            if (!IsValid || package == null || !Alie) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, ExtraEffectHandler.TriggerType.Heal);
        }

        /// <summary>
        /// Applies a stat package to the contacted object if valid.
        /// </summary>
        /// <param name="package">The stat package to apply.</param>
        public void Apply(StatPackage package)
        {
            if (!IsValid || package == null) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, ExtraEffectHandler.TriggerType.Stat);
        }

        /// <summary>
        /// Applies a buff to the contacted object if valid and allowed by ally logic.
        /// </summary>
        /// <param name="buff">The buff to apply.</param>
        public void Apply(IBuff buff)
        {
            if (!IsValid || buff == null) return;
            if (!buff.AffectsAllies && Alie) return;
            Hub.Apply(buff);
        }

        /// <summary>
        /// Applies a stat buff data to the contacted object if valid.
        /// </summary>
        /// <param name="statBuffData">The stat buff data to apply.</param>
        public void Apply(StatBuffData statBuffData)
        {
            if (!IsValid || !statBuffData) return;
            IBuff buff = statBuffData.GetBuff(Hub);
            Apply(buff);
        }

        /// <summary>
        /// Applies a damage buff data to the contacted object if valid.
        /// </summary>
        /// <param name="damageBuffData">The damage buff data to apply.</param>
        /// <param name="source">The source GameObject.</param>
        /// <param name="statGroup">Optional stat group context.</param>
        public void Apply(DamageBuffData damageBuffData, GameObject source, IStatGroup statGroup = null)
        {
            if (!IsValid || !damageBuffData) return;
            IBuff buff = damageBuffData.GetBuff(Hub, source, statGroup);
            Apply(buff);
        }

        /// <summary>
        /// Applies a healing buff data to the contacted object if valid.
        /// </summary>
        /// <param name="healingBuffData">The healing buff data to apply.</param>
        public void Apply(HealingBuffData healingBuffData)
        {
            if (!IsValid || !healingBuffData) return;
            IBuff buff = healingBuffData.GetBuff(Hub);
            Apply(buff);
        }
    }
}