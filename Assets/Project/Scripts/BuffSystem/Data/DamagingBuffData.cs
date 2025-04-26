using Project.Scripts.EffectSystem.Effects;
using Project.Scripts.EffectSystem.Effects.Type;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "DamagingBuff", menuName = "BuffSystem/Data/DamagingBuff")]
    public class DamagingBuffData : BuffData<AttackType>
    {
    }
}