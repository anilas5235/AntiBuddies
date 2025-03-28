using UnityEngine;

namespace DamageSystem
{
    public interface IDamageDealer
    {
        protected void Attack(GameObject target, Attack attack);
    }
}