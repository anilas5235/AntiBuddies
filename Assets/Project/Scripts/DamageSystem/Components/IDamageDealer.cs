using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public interface IDamageDealer
    {
        protected void Attack(GameObject target, Attack attack);
    }
}