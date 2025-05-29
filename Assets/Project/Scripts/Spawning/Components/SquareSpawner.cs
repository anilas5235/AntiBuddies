using UnityEngine;

namespace Project.Scripts.Spawning.Components
{
    public class SquareSpawner : Spawner
    {
        [SerializeField] private float size = 5f;

        protected override Vector2 GetSpawnPosition(Vector2 basePosition)
        {
            return SpawnUtils.RandomPointInSquare(basePosition, size);
        }

        public override void SetUp(Batch batch)
        {
            base.SetUp(batch);
            size = batch.spawnRadius;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireCube(transform.position, new Vector3(size, size, 1));
        }
#endif
    }
}