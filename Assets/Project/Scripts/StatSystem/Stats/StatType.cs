using UnityEngine;

namespace Project.Scripts.StatSystem.Stats
{
    /// <summary>
    /// ScriptableObject representing a type of stat, including value range and percentage flag.
    /// </summary>
    [CreateAssetMenu(fileName = "StatType", menuName = "EffectSystem/Types/StatType")]
    public class StatType : ScriptableObject
    {
        /// <summary>
        /// The description of the stat type.
        /// </summary>
        [SerializeField] private string description = "no description jet";

        /// <summary>
        /// Gets the name of the stat type from the ScriptableObject.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the description of the stat type.
        /// </summary>
        public string Description => description;

        /// <summary>
        /// Indicates if the stat is a percentage value.
        /// </summary>
        [SerializeField] private bool isPercentage;

        public bool IsPercentage => isPercentage;

        /// <summary>
        /// The maximum allowed value for this stat.
        /// </summary>
        [SerializeField] private int maxValue = int.MaxValue;

        /// <summary>
        /// The minimum allowed value for this stat.
        /// </summary>
        [SerializeField] private int minValue = int.MinValue;

        /// <summary>
        /// Gets the maximum allowed value for this stat.
        /// </summary>
        public int MaxValue => maxValue;

        /// <summary>
        /// Gets the minimum allowed value for this stat.
        /// </summary>
        public int MinValue => minValue;

        private void OnValidate()
        {
            // If the stat is a percentage, clamp minValue to -100
            if (isPercentage)
            {
                minValue = -100;
            }
            else
            {
                minValue = int.MinValue;
                maxValue = int.MaxValue;
            }
        }
    }
}