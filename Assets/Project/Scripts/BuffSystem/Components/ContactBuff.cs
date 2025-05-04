using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Components
{
    public class ContactBuff : ContactEffectSource
    {
        [SerializeField] private StatBuffData statBuffData;
        [SerializeField] protected AlieGroup alieGroup;

        protected override void HandleContact(GameObject other)
        {
            BuffManager buffManager = other.GetComponent<BuffManager>();
            if (!buffManager)
            {
                return;
            }
            ITarget<EffectPackage<StatType>> target = other.GetComponent<ITarget<EffectPackage<StatType>>>();
            buffManager.AddBuff(statBuffData.GetBuff(target, gameObject, alieGroup));
        }
    }
}