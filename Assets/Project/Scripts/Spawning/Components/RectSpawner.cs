using UnityEngine;

namespace Project.Scripts.Spawning.Components
{
    public class RectSpawner: Spawner
    {
        [SerializeField] private Vector2 size = new(5f, 5f);

        public Vector2 Size
        {
            get => size;
            protected set
            {
                if (value.x <= 0 || value.y <= 0)
                {
                    Debug.LogError("Size must be positive.");
                    return;
                }
                size = value;
            }
        }

        protected override Vector2 GetSpawnPosition(Vector2 basePosition)
        {
            return SpawnUtils.RandomPointInRect(basePosition, size);
        }
        
        public override void SetUp(Batch batch)
        {
            base.SetUp(batch);
            size = new Vector2(batch.spawnRadius * 2, batch.spawnRadius * 2);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 1));
        }
#endif
    }
}