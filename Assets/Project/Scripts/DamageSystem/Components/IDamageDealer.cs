using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageDealer
    {
        public void Attack(GameObject target, AttackPackage attackPackage);
    }
}