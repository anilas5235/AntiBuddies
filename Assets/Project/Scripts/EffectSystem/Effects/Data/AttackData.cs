using System;
using Project.Scripts.EffectSystem.Components;
using Project.Scripts.EffectSystem.Effects.Interfaces;
using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.StatSystem;
using Project.Scripts.StatSystem.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "EffectSystem/Data/Attack")]
    public class AttackData : EffectData<AttackType>
    {
        
    }
}