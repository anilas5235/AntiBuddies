using UnityEngine;

namespace Project.Scripts.EffectSystem.Effects.Data.Type
{
    /// <summary>
    /// ScriptableObject representing a type of healing effect.
    /// </summary>
    [CreateAssetMenu(fileName = "HealType", menuName = "EffectSystem/Types/HealType")]
    public class HealType : EffectType
    {
        /// <summary>
        /// The color associated with this heal type.
        /// </summary>
        [SerializeField] private Color color = Color.green;

        /// <summary>
        /// Gets the color associated with this heal type.
        /// </summary>
        public Color Color => color;
    }
}