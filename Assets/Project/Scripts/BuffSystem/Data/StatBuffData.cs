using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "StatBuffData", menuName = "BuffSystem/Data/StatBuff")]
    public class StatBuffData : BuffData
    {
        [SerializeField] private int amount;
        [SerializeField] private StatType statType;
        [SerializeField] private bool affectsAllies;

        public IBuff GetBuff(IPackageHub hub)
        {
            StatPackage package = new(amount, statType, StatPackage.StatModification.TempValue);
            return new StatBuff(package, duration, hub, GetStackBehavior(), GetTickBehavior(), affectsAllies);
        }
    }
}