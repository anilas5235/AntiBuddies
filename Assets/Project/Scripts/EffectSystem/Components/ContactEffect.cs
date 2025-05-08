using System.Collections.Generic;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Data;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ContactEffect<T> : ContactTrigger where T : EffectType
    {
        [SerializeField] private List<EffectData<T>> effectData;
        [SerializeField] protected AlieGroup alieGroup;

        protected override void HandleContact(GameObject other)
        {
            if (effectData.Count < 1) return;

            ITarget<EffectPackage<T>> target = other.GetComponent<ITarget<EffectPackage<T>>>();
            foreach (EffectData<T> effect in effectData)
            {
                if (!effect) continue;
                target.Apply(effect.GetPackage(gameObject, alieGroup));
            }
        }

        public void Add(EffectData<T> data)
        {
            if (!data) return;
            effectData.Add(data);
        }

        public void Clear()
        {
            effectData.Clear();
        }
    }
}