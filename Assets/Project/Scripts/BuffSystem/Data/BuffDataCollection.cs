using System;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [Serializable]
    public class BuffDataCollection<T> : DataCollection<BuffData<T>>,IApplyDynamicEffect where T : EffectType
    {
        public int Apply(GameObject other, AlieGroup alieGroup, IStatGroup statGroup, GameObject source)
        {
            int applies = 0;
            if (IsEmpty) return applies;

            BuffManager buffManager = other.GetComponent<BuffManager>();
            IPackageTarget<T> target = other.GetComponent<IPackageTarget<T>>();
            if (!buffManager || target == null) return applies;

            foreach (BuffData<T> buffData in Data)
            {
                if (!buffData.CanBeAppliedToAlly && target.IsAlie(alieGroup)) continue;
                buffManager.AddBuff(buffData.GetBuff(target, source, statGroup));
                applies++;
            }

            return applies;
        }
    }
}