using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects
{
    [CreateAssetMenu(fileName = "HealType", menuName = "EffectSystem/Types/HealType")]
    public class HealType : EffectType
    {
        [SerializeField] private Color color = Color.green;
        
        public Color Color => color;
    }
}