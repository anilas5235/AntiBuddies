using System;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public class DynamicEffectCollection<T> : DataCollection<EffectDef<T>>, IApplyDynamicEffect where T : EffectType
    {
        public int Apply(GameObject other, AlieGroup alieGroup, IStatGroup statGroup, GameObject source)
        {
            int applies = 0;
            if (IsEmpty) return applies;

            IPackageTarget<T> target = other.GetComponent<IPackageTarget<T>>();
            if (target == null || target.IsAlie(alieGroup)) return applies;

            foreach (EffectDef<T> effect in Data)
            {
                target.Apply(effect.CreatePackage(source, statGroup));
                applies++;
            }

            return applies;
        }
    }
}