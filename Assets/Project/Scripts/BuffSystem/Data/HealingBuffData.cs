using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "HealingBuffData", menuName = "BuffSystem/Data/HealingBuff")]
    public class HealingBuffData : BuffData
    {
        [SerializeField] private HealDefinition healing;

        public IBuff GetBuff(IPackageHub hub)
        {
            if (hub == null) return null;
            HealPackage package = healing.CreatePackage();
            return new HealBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior());
        }
    }
}