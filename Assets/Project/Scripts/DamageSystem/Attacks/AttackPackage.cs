using System;
using System.Collections.Generic;
using Project.Scripts.DamageSystem.Components;

namespace Project.Scripts.DamageSystem.Attacks
{
    [Serializable]
    public class AttackPackage
    {
        private List<IDamage> _damageComponents;
        private IDamageDealer _sender;

        public List<IDamage> DamageComponents => _damageComponents;
        public IDamageDealer Sender => _sender;

        public AttackPackage(List<IDamage> damageComponents)
        {
            _damageComponents = damageComponents ?? new List<IDamage>();
            _sender = null;
        }

        public AttackPackage(IDamage attackDataComponent) 
            : this(new List<IDamage> { attackDataComponent })
        {
        }

        public void SetSender(IDamageDealer sender)
        {
            _sender = sender;
        }

        public void AddDamageComponent(IDamage damageComponent)
        {
            if (damageComponent != null)
            {
                _damageComponents.Add(damageComponent);
            }
        }
    }
}
