using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public interface IPoolable
    {
        public GameObjectPool Pool { get; set; }

        public GameObject GetGameObject();

        internal void Init(GameObjectPool pool)
        {
            Pool = pool;
        }

        public void Activate();

        public void Deactivate();

        public void Reset();

        public void ReturnToPool();
        
        public void SetTransform(Vector3 position, Quaternion rotation);
    }
}