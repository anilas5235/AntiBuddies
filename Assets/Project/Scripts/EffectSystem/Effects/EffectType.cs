using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "NewEffectType", menuName = "EffectsSys/EffectType")]
    public class EffectType : ScriptableObject
    {
        [SerializeField] private EffectCategory effectCategory;
        [SerializeField] private string description = "no description jet";
        [SerializeField] private Color color = Color.white;
        [SerializeField] private bool affectedByPercentModifier;
        [SerializeField] private bool affectedByFlatModifier;

        public EffectCategory EffectCategory => effectCategory;
        public string Name => name;
        public string Description => description;
        public Color Color => color;
        public bool AffectedByPercentModifier => affectedByPercentModifier;
        public bool AffectedByFlatModifier => affectedByFlatModifier;
    }
}