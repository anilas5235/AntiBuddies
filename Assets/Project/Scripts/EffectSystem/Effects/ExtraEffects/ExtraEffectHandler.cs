using System.Collections.Generic;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.ExtraEffects
{
    /// <summary>
    /// Manages extraEffects that are associated with an entity.
    /// These effects are triggered by scenarios like dodging or dealing damage to a target.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public class ExtraEffectHandler : MonoBehaviour, INeedStatGroup
    {
        /// <summary>
        /// List of extraEffects to execute.
        /// </summary>
        private readonly List<IExtraEffect> _components = new();

        /// <summary>
        /// The stat group associated with this handler.
        /// </summary>
        private IStatGroup _statGroup;

        /// <summary>
        /// Adds an extra effect to the handler.
        /// </summary>
        /// <param name="extraEffect">The extraEffect to add.</param>
        public void AddComponent(IExtraEffect extraEffect)
        {
            if (extraEffect == null) return;
            _components.Add(extraEffect);
        }

        /// <summary>
        /// Removes an extra effect from the handler.
        /// </summary>
        /// <param name="extraEffect">The extraEffect to remove.</param>
        public void RemoveComponent(IExtraEffect extraEffect)
        {
            if (extraEffect == null) return;
            _components.Remove(extraEffect);
        }

        /// <summary>
        /// Executes all extraEffects that should be applied for the given trigger mode.
        /// </summary>
        /// <param name="hub">The package hub to apply effects to.</param>
        /// <param name="mode">The trigger mode.</param>
        public void Execute(IPackageHub hub, TriggerType mode)
        {
            foreach (IExtraEffect component in _components)
            {
                if (component.ApplyCondition(mode)) component.ApplyTo(hub, _statGroup);
            }
        }

        /// <summary>
        /// Removes all extraEffects from the handler.
        /// </summary>
        public void ClearEffects()
        {
            _components.Clear();
        }

        public void OnStatInit(IStatGroup statGroup)
        {
            _statGroup = statGroup;
        }

        /// <summary>
        /// Specifies the trigger mode for extra effects in the effect handler.
        /// </summary>
        public enum TriggerType
        {
            Damage,
            TakeDamage,
            Heal,
            SelfDodge,
            Critical,
        }
    }
}