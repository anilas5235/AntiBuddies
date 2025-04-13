using Project.Scripts.EffectSystem.Effects.Heal;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewHealBuff", menuName = "BuffSystem/HealBuff")]

    public class HealBuffData : BuffData<IHealable, IHeal>
    {
        
    }
}