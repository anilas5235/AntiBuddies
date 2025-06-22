using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    /// <summary>
    /// ScriptableObject for configuring a damaging buff.
    /// </summary>
    [CreateAssetMenu(fileName = "DamagingBuff", menuName = "BuffSystem/Data/DamagingBuff")]
    public class DamageBuffData : BuffData
    {
        /// <summary>
        /// The damage definition used to create the damage package.
        /// </summary>
        [SerializeField] private DamageDefinition damage;

        /// <summary>
        /// Creates a DamageBuff instance from this data.
        /// </summary>
        /// <param name="hub">The package hub to apply effects to.</param>
        /// <param name="source">The source GameObject for the damage.</param>
        /// <param name="statComponent">The stat group for calculations.</param>
        /// <returns>A new <see cref="DamageBuff"/> instance, or null if hub is null.</returns>
        public DamageBuff GetBuff(IPackageHub hub, GameObject source, IStatGroup statComponent)
        {
            if (hub == null) return null;
            DamagePackage package = damage.CreatePackage(source, statComponent);
            return new DamageBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior());
        }
    }
}