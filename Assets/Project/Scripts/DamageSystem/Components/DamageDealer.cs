using Project.Scripts.DamageSystem.Attacks;
using UnityEngine;

namespace Project.Scripts.DamageSystem.Components
{
    public class DamageDealer : MonoBehaviour, IDamageDealer
    {
        public DamageInfo damageInfo = new(1, DamageType.Physical);

        public void Attack(GameObject target)
        {
            DamageUtils.Attack(target, damageInfo, this);
        }
    }
}
