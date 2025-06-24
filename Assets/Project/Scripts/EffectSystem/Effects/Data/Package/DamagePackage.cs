using System;
using Project.Scripts.EffectSystem.Effects.Data.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    /// <summary>
    /// Represents a package containing all data required to apply a damage effect.
    /// </summary>
    [Serializable]
    public class DamagePackage : EffectPackage
    {
        /// <summary>
        /// The type of damage this package represents.
        /// </summary>
        [SerializeField] private DamageType damageType;

        /// <summary>
        /// The source GameObject that caused the damage.
        /// </summary>
        [SerializeField] private GameObject source;

        /// <summary>
        /// Indicates if the damage is a critical hit.
        /// </summary>
        private bool _isCritical;

        /// <summary>
        /// Event triggered when damage is applied, passing the amount dealt.
        /// </summary>
        public event Action<int> OnDamageApplied;

        /// <param name="amount">The amount of damage.</param>
        /// <param name="source">The source GameObject of the damage.</param>
        /// <param name="damageType">The type of damage.</param>
        /// <param name="isCritical">Whether the damage is critical.</param>
        public DamagePackage(int amount, GameObject source, DamageType damageType, bool isCritical = false) :
            base(amount)
        {
            this.damageType = damageType;
            _isCritical = isCritical;
            this.source = source;
        }

        /// <summary>
        /// Gets the type of damage.
        /// </summary>
        public DamageType DamageType => damageType;

        /// <summary>
        /// Gets the source GameObject of the damage.
        /// </summary>
        public GameObject Source => source;

        /// <summary>
        /// Invokes the <see cref="OnDamageApplied"/> event with the specified amount.
        /// </summary>
        /// <param name="amount">The amount of damage applied.</param>
        public void DamageApplied(int amount)
        {
            OnDamageApplied?.Invoke(amount);
        }

        /// <summary>
        /// Scales the incoming damage based on the receiver's stats.
        /// </summary>
        /// <param name="damage">The base damage value.</param>
        /// <param name="statGroup">The stat group of the receiver.</param>
        /// <returns>The scaled damage value.</returns>
        public int ReceptionScale(int damage, IStatGroup statGroup)
        {
            return damageType.ReceptionScale(damage, statGroup);
        }

        /// <summary>
        /// Gets the color representing the damage, using yellow for critical hits.
        /// </summary>
        /// <returns>The color representing the damage.</returns>
        public Color GetDamageColor()
        {
            // Use yellow for critical hits, otherwise use the damage type's color
            return _isCritical ? Color.yellow : damageType.Color;
        }
    }
}