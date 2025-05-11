using System;
using Project.Scripts.BuffSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [Serializable]
    public class BuffCollection<T> : DataCollection<BuffData<T>> where T : EffectType
    {
        public bool ApplyBuffs(GameObject other, StatComponent statComponent, GameObject source)
        {
            if (IsEmpty) return false;

            BuffManager buffManager = other.GetComponent<BuffManager>();
            if (!buffManager ) return false;
            IPackageTarget<T> target = other.GetComponent<IPackageTarget<T>>();
            if (target == null) return false;

            foreach (BuffData<T> buffData in Data)
            {
                buffManager.AddBuff(buffData.GetBuff(target, source, statComponent));
            }

            return true;
        }

        public override void Add(BuffData<T> t)
        {
            if(!t) return;
            base.Add(t);
        }
    }
}