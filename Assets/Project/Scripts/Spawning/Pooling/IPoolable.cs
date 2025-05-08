using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public interface IPoolable<T>
    {
        public GameObjectPool<T> Pool { get; set; }

        internal void Init(GameObjectPool<T> pool)
        {
            Pool = pool;
        }

        public void SetTransform(Vector3 position, Quaternion rotation);

        public void Activate(T data);

        public void Deactivate();

        public void Reset();

        public void ReturnToPool();
    }
}