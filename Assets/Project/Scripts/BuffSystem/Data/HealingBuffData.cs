using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    /// <summary>
    /// ScriptableObject for configuring a healing buff.
    /// </summary>
    [CreateAssetMenu(fileName = "HealingBuffData", menuName = "BuffSystem/Data/HealingBuff")]
    public class HealingBuffData : BuffData
    {
        /// <summary>
        /// The heal definition used to create the heal package.
        /// </summary>
        [SerializeField] private HealDefinition healing;

        /// <summary>
        /// Creates a HealBuff instance from this data.
        /// </summary>
        /// <param name="hub">The package hub to apply effects to.</param>
        /// <returns>A new <see cref="HealBuff"/> instance, or null if hub is null.</returns>
        public IBuff GetBuff(IPackageHub hub)
        {
            if (hub == null) return null;
            HealPackage package = healing.CreatePackage();
            return new HealBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior());
        }
    }
}