using Project.Scripts.EffectSystem.Components.Stats;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ResistanceComponent : MonoBehaviour
    {
        [Header("Damage Resistance")]
        public ClampedStat flatDamageReduction;
        public ClampedPercentStat physicalResistance;
        public ClampedPercentStat piercingResistance;
        
        [Header("Elemental Resistance")]
        public ClampedPercentStat fireResistance;
        public ClampedPercentStat iceResistance;
        public ClampedPercentStat lightningResistance;
        public ClampedPercentStat poisonResistance;
    }
}