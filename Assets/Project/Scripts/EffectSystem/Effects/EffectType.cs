using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "NewEffectType", menuName = "EffectsSys/EffectType")]
    public class EffectType : ScriptableObject
    {
        [SerializeField] private EffectCategory effectCategory;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private Color color;
        [SerializeField] private bool affectedByReduction;
        [SerializeField] private bool affectedByPercentageReduction;
        [SerializeField] private bool affectedByFlatDamageReduction;

        public EffectCategory EffectCategory => effectCategory;
        public string Name => title;
        public string Description => description;
        public Color Color => color;
        public bool AffectedByReduction => affectedByReduction;
        public bool AffectedByPercentageReduction => affectedByPercentageReduction;
        public bool AffectedByFlatDamageReduction => affectedByFlatDamageReduction;
    }
}