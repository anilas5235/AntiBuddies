using UnityEngine;

namespace Project.Scripts.Spawning
{
    [CreateAssetMenu(fileName = "Batch", menuName = "SpawnSystem/Batch")]
    public class Batch : ScriptableObject
    {
        public GameObject enemyPrefab;
        public int spawnPerBatch;
        public float spawnDelay;
        public int numberOfBatches;
        public float spawnRadius;
        public float initialDelay;
    }
}