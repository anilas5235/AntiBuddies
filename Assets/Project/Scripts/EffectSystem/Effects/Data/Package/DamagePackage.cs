using System;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Package
{
    [Serializable]
    public class DamagePackage : EffectPackage
    {
        [SerializeField] private DamageType damageType;
        [SerializeField] private GameObject source;
        public event Action<int> OnDamageApplied; 

        public DamagePackage(int amount, GameObject source, DamageType damageType) : base(amount)
        {
            this.damageType = damageType;
            this.source = source;
        }

        public DamageType DamageType => damageType;
        public GameObject Source => source;
        
        public void DamageApplied(int amount)
        {
            OnDamageApplied?.Invoke(amount);
        }

        public int ReceptionScale(int damage, StatComponent statComponent)
        {
            return damageType.ReceptionScale(damage, statComponent);
        }
    }
}