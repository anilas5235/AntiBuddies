using UnityEngine;

namespace Project.Scripts.UtilComponents
{
    /// <summary>
    /// Selects a random sprite from a list and assigns it to the SpriteRenderer on Awake.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SelectRandomSprite : MonoBehaviour
    {
        /// <summary>
        /// Array of possible sprites to select from.
        /// </summary>
        [SerializeField] private Sprite[] sprites;

        private void Awake()
        {
            if (sprites.Length == 0)
            {
                Debug.LogError("No sprites assigned to SelectRandomSprite.");
                return;
            }

            // Pick a random sprite and assign it to the SpriteRenderer.
            int randomIndex = Random.Range(0, sprites.Length);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = sprites[randomIndex];
            // Optionally adjust sorting order for visual variety.
            spriteRenderer.sortingOrder -= randomIndex;
        }
    }
}