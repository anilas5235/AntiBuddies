using UnityEngine;

namespace Project.Scripts.Spawning.Pooling
{
    public abstract class PoolableMono : MonoBehaviour, IPoolable
    {
        private void Awake()
        {
            Reset();
        }

        public GameObjectPool Pool { get; set; }

        public GameObject GetGameObject() => gameObject;

        public virtual void Activate() => gameObject.SetActive(true);

        public virtual void Deactivate() => gameObject.SetActive(false);

        public abstract void Reset();

        public void ReturnToPool() => Pool.AddToPool(this);

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}