using JetBrains.Annotations;
using Project.Scripts.WeaponSystem.Attack;
using Project.Scripts.WeaponSystem.Targeting;
using UnityEngine;

namespace Project.Scripts.WeaponSystem
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField,NotNull] private TargetingBehaviour targetingBehaviour;
        [SerializeField,NotNull] private AttackBehaviour attackBehaviour;
        
        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void DestroyWeapon()
        {
            Destroy(gameObject);
        }
    }
}