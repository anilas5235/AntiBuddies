using System;
using Project.Scripts.EffectSystem.Components.Stats.StatBehaviour;

namespace Project.Scripts.EffectSystem.Components.Stats
{
    [Serializable]
    public class PercentStat : BaseStat
    {
        public PercentStat(int statValue = 0) : base(new PercentStatBehaviour(), statValue)
        {
        }
    }
}