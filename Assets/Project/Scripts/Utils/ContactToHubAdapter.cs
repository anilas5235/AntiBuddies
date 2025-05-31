using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using UnityEngine;

namespace Project.Scripts.Utils
{
    public class ContactToHubAdapter : IPackageHub
    {
        private GameObject ContactObject { get; }
        private EffectPipeline EffectPipeline { get; }
        private IPackageHub Hub { get; }
        public bool Alie { get; }

        public bool IsValid { get; } = true;

        public ContactToHubAdapter(GameObject contactObject, AllyGroup allyGroup, EffectPipeline effectPipeline = null)
        {
            EffectPipeline = effectPipeline;
            ContactObject = contactObject;
            if (!ContactObject)
            {
                IsValid = false;
                return;
            }

            Hub = contactObject.GetComponent<IPackageHub>();
            if (Hub == null)
            {
                IsValid = false;
                return;
            }

            Alie = Hub.IsAlie(allyGroup);
        }

        public void Apply(DamagePackage package)
        {
            if (!IsValid || package == null || Alie) return;
            Hub.Apply(package);
            EffectPipeline?.Execute(Hub, EffectPipelineMode.Damage);
        }

        public void Apply(HealPackage package)
        {
            if (!IsValid || package == null || !Alie) return;
            Hub.Apply(package);
            EffectPipeline?.Execute(Hub, EffectPipelineMode.Heal);
        }

        public void Apply(StatPackage package)
        {
            if (!IsValid|| package == null) return;
            Hub.Apply(package);
            EffectPipeline?.Execute(Hub, EffectPipelineMode.Stat);
        }

        public bool IsAlie(AllyGroup group)
        {
            if (!IsValid) return false;
            return Hub.IsAlie(group);
        }

        public void Apply(IBuff buff)
        {
            if (!IsValid|| buff == null) return;
            if (!buff.AffectsAllies && Alie) return;
            buff.Hub = Hub;
            Hub.Apply(buff);
        }
    }
}