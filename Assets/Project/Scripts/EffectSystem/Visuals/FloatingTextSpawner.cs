using Project.Scripts.EffectSystem.Effects.Type;
using Project.Scripts.Spawning.Pooling;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.EffectSystem.Visuals
{
    /// <summary>
    /// Spawns floating text visuals at specified positions, using pooling for efficiency.
    /// </summary>
    public class FloatingTextSpawner : Singleton<FloatingTextSpawner>
    {
        /// <summary>
        /// The prefab used for floating text visuals.
        /// </summary>
        [SerializeField] private GameObject floatingNumberPrefab;
        /// <summary>
        /// The duration each floating text is displayed.
        /// </summary>
        [SerializeField] private float displayDuration = 1.0f;
        /// <summary>
        /// The offset applied to the spawn position.
        /// </summary>
        [SerializeField] private Vector2 offset = new(0, 0.5f);
        /// <summary>
        /// If true, disables spawning of floating texts.
        /// </summary>
        [SerializeField] private bool stopSpawn;
        
        private GameObjectPool _floatingNumberPool;

        private void OnEnable()
        {
            _floatingNumberPool = GlobalPools.Instance.GetPoolFor(floatingNumberPrefab);
        }

        /// <summary>
        /// Spawns a floating text at the source position with the specified text and color.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="source">The source GameObject for position.</param>
        public void SpawnFloatingText(string text, Color color, GameObject source)
        {
            if (stopSpawn) return;
            if (!_floatingNumberPool)
            {
                Debug.LogWarning("FloatingTextPool is null");
                return;
            }

            FloatingText textInstance = (FloatingText)_floatingNumberPool.GetObject();
            if (!textInstance)
            {
                Debug.LogWarning("FloatingTextPool returned null");
                return;
            }

            textInstance.SetTransform(source.transform.position + (Vector3)offset, Quaternion.identity);
            textInstance.Setup(new FloatingTextData(text, color, displayDuration));
        }

        /// <summary>
        /// Spawns a floating text at the source position with the specified value and color.
        /// </summary>
        /// <param name="num">The value to display.</param>
        /// <param name="color">The color of the text.</param>
        /// <param name="source">The source GameObject for position.</param>
        public void SpawnFloatingNumber(int num, Color color, GameObject source)
        {
            SpawnFloatingText(num.ToString(), color, source);
        }
    }
}