using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageDealer
    {
        public void Attack(GameObject target, Attack attack);
    }
}