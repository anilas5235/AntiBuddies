using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    public class SelectRandomSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;

        private void Awake()
        {
            if (sprites.Length == 0)
            {
                Debug.LogError("No sprites assigned to SelectRandomSprite.");
                return;
            }

            int randomIndex = Random.Range(0, sprites.Length);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                spriteRenderer.sprite = sprites[randomIndex];
                spriteRenderer.sortingOrder -= randomIndex;
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on the GameObject.");
            }
        }
    }
}