using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    /// <summary>
    /// Interface for objects that can be pooled and reused by a GameObjectPool.
    /// </summary>
    /// <remarks>Author: Niklas Borchers</remarks>
    public interface IPoolable
    {
        /// <summary>
        /// The pool this object belongs to.
        /// </summary>
        public GameObjectPool Pool { get; set; }

        /// <summary>
        /// Gets the GameObject associated with this poolable.
        /// </summary>
        public GameObject GetGameObject();

        internal void Init(GameObjectPool pool)
        {
            Pool = pool;
        }

        /// <summary>
        /// Activates the object for use.
        /// </summary>
        public void Activate();

        /// <summary>
        /// Deactivates the object when returned to the pool.
        /// </summary>
        public void Deactivate();

        /// <summary>
        /// Resets the object's state before reuse.
        /// </summary>
        public void Reset();

        /// <summary>
        /// Returns the object to its pool.
        /// </summary>
        public void ReturnToPool();
        
        /// <summary>
        /// Sets the transform of the object.
        /// </summary>
        /// <param name="position">The new position.</param>
        /// <param name="rotation">The new rotation.</param>
        public void SetTransform(Vector3 position, Quaternion rotation);
    }
}