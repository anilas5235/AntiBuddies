using System;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.ExtraEffects;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.WeaponSystem.Projectile
{
    /// <summary>
    /// Holds dynamic, runtime-specific settings for a projectile, such as direction, damage, buffs, effects, and source.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public class DynamicProjectileSettings
    {
        /// <summary>
        /// Creates a new instance with all dynamic projectile settings.
        /// </summary>
        /// <param name="damageDefinition">The damage definition for this projectile.</param>
        /// <param name="damageBuffData">The damage buff data to apply on contact.</param>
        /// <param name="extraEffectHandler">Handler for extra effects on contact.</param>
        /// <param name="direction">The direction to fire the projectile.</param>
        /// <param name="alliedGroup">The group the projectile belongs to.</param>
        /// <param name="additionalContacts">Additional allowed contacts beyond the base value.</param>
        /// <param name="source">The GameObject that spawned the projectile.</param>
        /// <param name="statGroup">The stat group for calculating effects.</param>
        public DynamicProjectileSettings(DamageDefinition damageDefinition, DamageBuffData damageBuffData,
            ExtraEffectHandler extraEffectHandler, Vector2 direction, AlliedGroup alliedGroup,
            int additionalContacts, GameObject source, IStatGroup statGroup)
        {
            DamageDefinition = damageDefinition;
            DamageBuffData = damageBuffData;
            ExtraEffectHandler = extraEffectHandler;
            Direction = direction;
            AlliedGroup = alliedGroup;
            AdditionalContacts = additionalContacts;
            Source = source;
            StatGroup = statGroup;
        }

        /// <summary>
        /// The damage definition for this projectile.
        /// </summary>
        public DamageDefinition DamageDefinition { get; }

        /// <summary>
        /// The damage buff data to apply on contact.
        /// </summary>
        public DamageBuffData DamageBuffData { get; }

        /// <summary>
        /// Handler for extra effects on contact.
        /// </summary>
        public ExtraEffectHandler ExtraEffectHandler { get; }

        /// <summary>
        /// The direction to fire the projectile.
        /// </summary>
        public Vector2 Direction { get; }

        /// <summary>
        /// The group the projectile belongs to.
        /// </summary>
        public AlliedGroup AlliedGroup { get; }

        /// <summary>
        /// Additional allowed contacts beyond the base value.
        /// </summary>
        public int AdditionalContacts { get; }

        /// <summary>
        /// The GameObject that spawned the projectile.
        /// </summary>
        public GameObject Source { get; }

        /// <summary>
        /// The stat group for calculating effects.
        /// </summary>
        public IStatGroup StatGroup { get; }

        /// <summary>
        /// Applies all configured effects to the contact GameObject.
        /// </summary>
        /// <param name="contact">The GameObject that was hit by the projectile.</param>
        /// <returns>True if effects were applied; false if the contact was invalid or allied.</returns>
        public bool ApplyEffects(GameObject contact)
        {
            // Create a processor to handle effects and check for valid, non-allied targets
            ContactEffectProcessor processor = new(contact, AlliedGroup, ExtraEffectHandler);
            if (!processor.IsValid || processor.Alie) return false;
            processor.Apply(DamageDefinition.CreatePackage(Source, StatGroup));
            processor.Apply(DamageBuffData, Source, StatGroup);
            return true;
        }
    }
}