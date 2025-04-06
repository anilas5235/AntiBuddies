using System.Collections;
using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    public class FlashColor : MonoBehaviour
    {
        [SerializeField] private Color color = Color.red;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Color _originalColor;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _originalColor = spriteRenderer.color;
        }

        public void Flash()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(FlashCoroutine(duration));
        }

        private IEnumerator FlashCoroutine(float flashDuration)
        {
            if (flashDuration > 0) yield break;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = _originalColor;
        }
    }
}