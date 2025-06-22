using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Enemy
{
    /// <summary>
    /// Applies a stat buff to other objects upon contact, using allied group filtering.
    /// </summary>
    public class StatBuffApplier : MonoBehaviour, IHandleContact
    {
        /// <summary>
        /// The stat buff data to apply on contact.
        /// </summary>
        [SerializeField] private StatBuffData statBuffData;
        /// <summary>
        /// The allied group this object belongs to, used for filtering contacts.
        /// </summary>
        [SerializeField] private AlliedGroup alliedGroup = AlliedGroup.Enemy;
        /// <summary>
        /// The extra effect handler for executing additional effects on contact.
        /// </summary>
        [SerializeField, Tooltip("Optional")] private ExtraEffectHandler extraEffectHandler;


        /// <inheritdoc/>
        public void HandleContact(GameObject contact)
        {
            // Adapter checks if the contact is valid and applies the stat buff if so.
            ContactToHubAdapter hubAdapter = new(contact, alliedGroup, extraEffectHandler);
            if (!hubAdapter.IsValid) return;
            hubAdapter.Apply(statBuffData);
        }
    }
}