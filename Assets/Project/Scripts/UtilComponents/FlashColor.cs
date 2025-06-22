using System.Collections;
using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    public class FlashColor : MonoBehaviour
    {
        [SerializeField] private Color color = new(1, .5f, .5f, .9f);
        [SerializeField, Range(.0001f, 3)] private float duration = 0.1f;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Color _originalColor;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _originalColor = spriteRenderer.color;
        }

        private void OnDisable()
        {
            Stop();
        }

        public void Flash()
        {
            Stop();
            if (!gameObject.activeInHierarchy) return;
            _coroutine = StartCoroutine(FlashCoroutine(duration));
        }

        private void Stop()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                spriteRenderer.color = _originalColor;
            }
        }

        private IEnumerator FlashCoroutine(float flashDuration)
        {
            if (flashDuration <= 0) yield break;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = _originalColor;
        }
    }
}