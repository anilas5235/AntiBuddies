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
        private IPackageHub Hub { get; }
        public bool Alie { get; }

        public bool IsValid { get; } = true;

        public ContactToHubAdapter(GameObject contactObject, AlieGroup alieGroup)
        {
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

            Alie = Hub.IsAlie(alieGroup);
        }

        public void Apply(DamagePackage package)
        {
            if (!IsValid || package == null || Alie) return;
            Hub.Apply(package);
        }

        public void Apply(HealPackage package)
        {
            if (!IsValid || package == null || !Alie) return;
            Hub.Apply(package);
        }

        public void Apply(StatPackage package)
        {
            if (!IsValid|| package == null) return;
            Hub.Apply(package);
        }

        public bool IsAlie(AlieGroup group)
        {
            if (!IsValid) return false;
            return Hub.IsAlie(group);
        }

        public void Apply(IBuff buff)
        {
            if (!IsValid|| buff == null) return;
            if (!buff.CanBeAppliedToAlly && Alie) return;
            buff.Hub = Hub;
            Hub.Apply(buff);
        }
    }
}