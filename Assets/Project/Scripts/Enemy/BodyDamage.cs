using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.StatSystem;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Enemy
{
    /// <summary>
    /// Applies damage and optional buffs to other objects upon contact, using stat group context.
    /// </summary>
    public class BodyDamage : MonoBehaviour, IHandleContact, INeedStatGroup
    {
        /// <summary>
        /// The definition of the damage to apply on contact.
        /// </summary>
        [SerializeField] private DamageDefinition damage = new();
        /// <summary>
        /// Optional buff data to apply along with the damage.
        /// </summary>
        [SerializeField] private DamageBuffData buffData;
        /// <summary>
        /// The allied group this object belongs to, used for filtering contacts.
        /// </summary>
        [SerializeField] private AlliedGroup alliedGroup = AlliedGroup.Enemy;
        /// <summary>
        /// The extra effect handler for executing additional effects on contact.
        /// </summary>
        [SerializeField, Tooltip("Optional")] private ExtraEffectHandler extraEffectHandler;
        /// <summary>
        /// The stat group associated with this object.
        /// </summary>
        private IStatGroup _statGroup;


        /// <inheritdoc/>
        public void HandleContact(GameObject contact)
        {
            // Adapter checks if the contact is valid and applies damage and buffs if so.
            ContactToHubAdapter hubAdapter = new(contact, alliedGroup, extraEffectHandler);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(damage.CreatePackage(gameObject, _statGroup));
            hubAdapter.Apply(buffData, gameObject, _statGroup);
        }

        /// <inheritdoc/>
        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
        }
    }
}