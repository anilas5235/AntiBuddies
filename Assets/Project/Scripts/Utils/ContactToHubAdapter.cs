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
        private ExtraEffectHandler ExtraEffectHandler { get; }
        private IPackageHub Hub { get; }
        public bool Alie { get; }

        public bool IsValid { get; } = true;

        public ContactToHubAdapter(GameObject contactObject, AlliedGroup alliedGroup, ExtraEffectHandler extraEffectHandler = null)
        {
            ExtraEffectHandler = extraEffectHandler;
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

            Alie = Hub.IsAlie(alliedGroup);
        }

        public void Apply(DamagePackage package)
        {
            if (!IsValid || package == null || Alie) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, EffectTrigger.Damage);
        }

        public void Apply(HealPackage package)
        {
            if (!IsValid || package == null || !Alie) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, EffectTrigger.Heal);
        }

        public void Apply(StatPackage package)
        {
            if (!IsValid|| package == null) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, EffectTrigger.Stat);
        }

        public bool IsAlie(AlliedGroup group)
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