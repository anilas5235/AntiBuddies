using Project.Scripts.EffectSystem.Effects;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "HealingBuffData", menuName = "BuffSystem/Data/HealingBuff")]
    public class HealingBuffData : BuffData<HealType>
    {
    }
}