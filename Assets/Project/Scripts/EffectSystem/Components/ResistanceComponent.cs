using UnityEngine;

namespace Project.Scripts.EffectSystem.Components
{
    public class ResistanceComponent : MonoBehaviour
    {
        [Header("Damage Reduction")]
        public Stat flatDamageReduction;
        public Stat physicalResistance;
        public Stat piercingResistance;
        
        [Header("Elemental Resistance")]
        public Stat fireResistance;
    }
}