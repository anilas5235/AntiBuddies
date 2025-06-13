using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "DamagingBuff", menuName = "BuffSystem/Data/DamagingBuff")]
    public class DamageBuffData : BuffData
    {
        [SerializeField] private DamageDefinition damage;
        public DamageBuff GetBuff(IPackageHub hub, GameObject source, IStatGroup statComponent)
        {
            if (hub == null) return null;
            DamagePackage package = damage.CreatePackage(source, statComponent);
            return new DamageBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior());
        }
    }
}