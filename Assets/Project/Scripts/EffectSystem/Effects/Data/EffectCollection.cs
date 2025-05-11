using System;
using System.Collections.Generic;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [Serializable]
    public class EffectCollection<T> : DataCollection<EffectDef<T>> where T : EffectType
    {
        public bool ApplyEffects(GameObject other, AlieGroup alieGroup, StatComponent statComponent, GameObject source)
        {
            if (IsEmpty) return false;

            IPackageTarget<T> target = other.GetComponent<IPackageTarget<T>>();
            if (target == null || target.IsAlie(alieGroup)) return false;

            foreach (EffectDef<T> effect in Data)
            {
                target.Apply(effect.CreatePackage(source, statComponent));
            }

            return true;
        }

        public override void Add(EffectDef<T> t)
        {
            if (!t.EffectType) return;
            base.Add(t);
        }

        public void Add(List<EffectDef<T>> list)
        {
            foreach (EffectDef<T> effectDef in list)
            {
                Add(effectDef);
            }
        }
    }
}