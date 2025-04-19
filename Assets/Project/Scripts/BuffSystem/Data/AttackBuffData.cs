using Project.Scripts.EffectSystem.Effects.Attacks;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewAttackBuff", menuName = "BuffSystem/AttackBuff")]

    public class AttackBuffData : BuffData<IDamageable,IAttack>
    {
        
    }
}