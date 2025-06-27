using UnityEngine;

namespace Project.Scripts.Spawning.Components
{
    public class CircleBatchSpawner : Spawner
    {
        [SerializeField] private float radius = 5f;

        protected override Vector2 GetSpawnPosition(Vector2 basePosition)
        {
            return SpawnUtils.RandomPointInCircle(basePosition, radius);
        }

        public override void SetUp(Batch batch)
        {
            base.SetUp(batch);
            radius = batch.spawnRadius;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}