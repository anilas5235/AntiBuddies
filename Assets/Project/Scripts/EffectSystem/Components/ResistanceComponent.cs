using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ResistanceComponent : MonoBehaviour
    {
        [Header("Damage Resistance")]
        public Stat flatDamageReduction;
        public PercentStat physicalResistance;
        public PercentStat piercingResistance;
        
        [Header("Elemental Resistance")]
        public PercentStat fireResistance;
        public PercentStat iceResistance;
        public PercentStat lightningResistance;
        public PercentStat poisonResistance;
    }
}