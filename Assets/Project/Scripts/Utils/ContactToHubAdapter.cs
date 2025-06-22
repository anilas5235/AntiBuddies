using Project.Scripts.BuffSystem.Buffs;
using Project.Scripts.BuffSystem.Data;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Data.Package;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.Utils
{
    public class ContactToHubAdapter
    {
        private GameObject ContactObject { get; }
        private ExtraEffectHandler ExtraEffectHandler { get; }
        private IPackageHub Hub { get; }
        public bool Alie { get; }

        public bool IsValid { get; } = true;

        public ContactToHubAdapter(GameObject contactObject, AlliedGroup alliedGroup,
            ExtraEffectHandler extraEffectHandler)
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
            if (!IsValid || package == null) return;
            Hub.Apply(package);
            ExtraEffectHandler?.Execute(Hub, EffectTrigger.Stat);
        }

        public void Apply(IBuff buff)
        {
            if (!IsValid || buff == null) return;
            if (!buff.AffectsAllies && Alie) return;
            Hub.Apply(buff);
        }

        public void Apply(StatBuffData statBuffData)
        {
            if (!IsValid || !statBuffData) return;
            IBuff buff = statBuffData.GetBuff(Hub);
            Apply(buff);
        }

        public void Apply(DamageBuffData damageBuffData, GameObject source, IStatGroup statGroup = null)
        {
            if (!IsValid || !damageBuffData) return;
            IBuff buff = damageBuffData.GetBuff(Hub, source, statGroup);
            Apply(buff);
        }

        public void Apply(HealingBuffData healingBuffData)
        {
            if (!IsValid || !healingBuffData) return;
            IBuff buff = healingBuffData.GetBuff(Hub);
            Apply(buff);
        }
    }
}