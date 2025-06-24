using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    /// <summary>
    /// ScriptableObject for configuring a stat-modifying buff.
    /// </summary>
    [CreateAssetMenu(fileName = "StatBuffData", menuName = "BuffSystem/Data/StatBuff")]
    public class StatBuffData : BuffData
    {
        /// <summary>
        /// The amount to modify the stat by.
        /// </summary>
        [SerializeField] private int amount;

        /// <summary>
        /// The type of stat to modify.
        /// </summary>
        [SerializeField] private StatType statType;

        /// <summary>
        /// Whether this buff affects allies.
        /// </summary>
        [SerializeField] private bool affectsAllies;

        /// <summary>
        /// Creates a StatBuff instance from this data.
        /// </summary>
        /// <param name="hub">The package hub to apply effects to.</param>
        /// <returns>A new <see cref="StatBuff"/> instance, or null if hub is null.</returns>
        public IBuff GetBuff(IPackageHub hub)
        {
            if (hub == null) return null;
            StatPackage package = new(amount, statType, StatPackage.StatModification.TempValue);
            return new StatBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior(), affectsAllies);
        }
    }
}