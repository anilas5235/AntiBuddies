using System.Collections.Generic;
using Project.Scripts.EffectSystem.Components.Stats;
using Project.Scripts.EffectSystem.Effects.Status;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class AmplificationComponent : MonoBehaviour
    {
        [SerializeField] private Dictionary<EffectType,PercentStat> amPercentStats = new();
    }
}