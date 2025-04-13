using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.BuffSystem.Data
{
    [CreateAssetMenu(fileName = "NewStatusBuff", menuName = "BuffSystem/StatusBuff")]

    public class StatusBuffData : BuffData<IStatusEffectable,IStatus>
    {
        
    }
}