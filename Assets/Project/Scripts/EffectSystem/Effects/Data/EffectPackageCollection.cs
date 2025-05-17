using System;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public class EffectPackageCollection<T> : DataCollection<EffectPackage<T>>, IApplyEffect where T : EffectType
    {
        public int Apply(GameObject other, AlieGroup alieGroup)
        {
            int applies = 0;
            if (IsEmpty) return applies;

            IPackageTarget<T> target = other.GetComponent<IPackageTarget<T>>();
            if (target == null || target.IsAlie(alieGroup)) return applies;

            foreach (EffectPackage<T> effect in Data)
            {
                target.Apply(effect);
                applies++;
            }

            return applies;
        }
    }
}