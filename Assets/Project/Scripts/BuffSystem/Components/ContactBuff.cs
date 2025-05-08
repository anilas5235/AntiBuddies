using System.Collections.Generic;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class ContactBuff<T> : ContactTrigger where T : EffectType
    {
        [SerializeField] private List<BuffData<T>> buffData;
        [SerializeField] protected AlieGroup alieGroup;

        protected override void HandleContact(GameObject other)
        {
            if (buffData.Count < 1) return;
            BuffManager buffManager = other.GetComponent<BuffManager>();
            if (!buffManager) return;
            ITarget<EffectPackage<T>> target = other.GetComponent<ITarget<EffectPackage<T>>>();
            foreach (BuffData<T> buffD in buffData)
            {
                if (!buffD) continue;
                buffManager.AddBuff(buffD.GetBuff(target, gameObject, alieGroup));
            }
        }

        public void Add(BuffData<T> data)
        {
            if (!data) return;
            buffData.Add(data);
        }
        
        public void Clear(){
            buffData.Clear();
        }
    }
}