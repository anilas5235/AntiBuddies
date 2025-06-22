using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.Enemy
{
    /// <summary>
    /// Handles temporary sprite effects with alpha fading over time, using pooling.
    /// </summary>
    public class TempSpriteEffect : LiveTimePoolableMono
    {
        /// <summary>
        /// The sprite renderer to apply the alpha effect to.
        /// </summary>
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Updates the sprite's alpha based on remaining lifetime.
        /// </summary>
        protected override void LiveTick()
        {
            // Fade out the sprite as timeToLive approaches zero.
            if(timeToLive > 1f) return;
            SetSpriteAlpha(timeToLive);
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            base.Reset();
            SetSpriteAlpha(1f);
        }

        /// <summary>
        /// Sets the alpha value of the sprite renderer.
        /// </summary>
        /// <param name="alpha">Alpha value to set (0-1).</param>
        private void SetSpriteAlpha(float alpha)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}