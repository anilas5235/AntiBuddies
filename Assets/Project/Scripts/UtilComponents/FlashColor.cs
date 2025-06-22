using System.Collections;
using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    /// <summary>
    /// Temporarily flashes a SpriteRenderer with a specified color for a short duration.
    /// </summary>
    public class FlashColor : MonoBehaviour
    {
        /// <summary>
        /// The color to flash.
        /// </summary>
        [SerializeField] private Color color = new(1, .5f, .5f, .9f);

        /// <summary>
        /// Duration of the flash in seconds.
        /// </summary>
        [SerializeField, Range(.0001f, 3)] private float duration = 0.1f;

        /// <summary>
        /// The SpriteRenderer to apply the flash effect to.
        /// </summary>
        [SerializeField] private SpriteRenderer spriteRenderer;

        /// <summary>
        /// The original color of the SpriteRenderer, saved to restore after flashing.
        /// /// </summary>
        private Color _originalColor;

        /// <summary>
        /// Coroutine reference for the flash effect, allowing it to be stopped if needed.
        /// </summary>
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _originalColor = spriteRenderer.color;
        }

        private void OnDisable()
        {
            Stop();
        }

        /// <summary>
        /// Triggers the flash effect. If already flashing, restarts the effect.
        /// </summary>
        public void Flash()
        {
            Stop();
            if (!gameObject.activeInHierarchy) return;
            _coroutine = StartCoroutine(FlashCoroutine(duration));
        }

        /// <summary>
        /// Stops the flash effect and restores the original color.
        /// </summary>
        private void Stop()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            spriteRenderer.color = _originalColor;
        }

        /// <summary>
        /// Coroutine that handles the timing and color change for the flash effect.
        /// </summary>
        /// <param name="flashDuration">How long to flash for.</param>
        private IEnumerator FlashCoroutine(float flashDuration)
        {
            if (flashDuration <= 0) yield break;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = _originalColor;
        }
    }
}