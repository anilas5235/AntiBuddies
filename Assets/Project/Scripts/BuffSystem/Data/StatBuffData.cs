using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Data.Definition;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "StatBuffData", menuName = "BuffSystem/Data/StatBuff")]
    public class StatBuffData : BuffData
    {
        [SerializeField] private StatDefinition statDef;
        public IBuff GetBuff(IPackageHub hub)
        {
            StatPackage package = statDef.CreatePackage();
            return new StatBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior());
        }
    }
}