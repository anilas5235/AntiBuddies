using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    /// <summary>
    /// Abstract MonoBehaviour implementing IPoolable for use with GameObjectPool.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public abstract class PoolableMono : MonoBehaviour, IPoolable
    {
        private void Awake()
        {
            Reset();
        }

        /// <inheritdoc/>
        public GameObjectPool Pool { get; set; }

        /// <inheritdoc/>
        public GameObject GetGameObject() => gameObject;

        /// <inheritdoc/>
        public virtual void Activate() => gameObject.SetActive(true);

        /// <inheritdoc/>
        public virtual void Deactivate() => gameObject.SetActive(false);

        /// <inheritdoc/>
        public abstract void Reset();

        /// <inheritdoc/>
        public void ReturnToPool() => Pool.AddToPool(this);

        /// <inheritdoc/>
        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}