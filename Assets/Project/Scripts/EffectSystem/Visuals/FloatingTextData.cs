using System;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    /// <summary>
    /// Holds the data required to display.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [Serializable]
    public class FloatingTextData
    {
        /// <summary>
        /// The text to display.
        /// </summary>
        public string text;

        /// <summary>
        /// The color of the floating text.
        /// </summary>
        public Color color;

        /// <summary>
        /// How long the floating text should be displayed.
        /// </summary>
        public float lifeTime;

        /// <param name="text">The text to display.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="lifeTime">The display duration.</param>
        public FloatingTextData(string text, Color color, float lifeTime)
        {
            this.text = text;
            this.color = color;
            this.lifeTime = lifeTime;
        }

        /// <summary>
        /// Returns the string representation of the floating text.
        /// </summary>
        /// <returns>The text to display.</returns>
        public override string ToString()
        {
            return text;
        }

        /// <summary>
        /// Gets the color for the floating text.
        /// </summary>
        /// <returns>The color value.</returns>
        public Color GetColor()
        {
            return color;
        }
    }
}