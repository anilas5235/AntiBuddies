using Project.Scripts.Spawning.Pooling;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    /// <summary>
    /// Displays a floating text visual at a specified position, used for damage, healing, etc.
    /// </summary>
    public class FloatingText : LiveTimePoolableMono
    {
        /// <summary>
        /// The range for randomizing the X offset of the floating text.
        /// </summary>
        private const float XOffsetRange = .7f;
        /// <summary>
        /// The fixed Y offset for the floating text.
        /// </summary>
        private const float YOffset = .5f;

        /// <summary>
        /// The data describing the floating text's appearance and value.
        /// </summary>
        [SerializeField] private FloatingTextData data;
        /// <summary>
        /// The TextMesh component used to display the text.
        /// </summary>
        [SerializeField] private TextMesh textMesh;

        /// <summary>
        /// Initializes the floating text with the specified data.
        /// </summary>
        /// <param name="floatingTextData">The data for the floating text.</param>
        public void Setup(FloatingTextData floatingTextData)
        {
            data = floatingTextData;

            Vector3 pos = transform.position;
            pos.z = -1;
            // Randomize X position for visual variety
            pos.x += Random.Range(-XOffsetRange, XOffsetRange);
            pos.y += YOffset;
            transform.position = pos;
            lifeTime = floatingTextData.lifeTime;
            
            UpdateText();
        }

        /// <summary>
        /// Updates the text and color of the floating text.
        /// </summary>
        private void UpdateText()
        {
            textMesh.text = data.ToString();
            textMesh.color = data.GetColor();
        }

        /// <summary>
        /// Resets the floating text to its default state when returned to the pool.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            textMesh.color = Color.white;
        }
    }
}