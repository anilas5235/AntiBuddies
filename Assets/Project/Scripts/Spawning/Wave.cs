using System;
using System.Collections.Generic;
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

    [CreateAssetMenu(fileName = "Wave", menuName = "SpawnSystem/Wave")]
    public class Wave : ScriptableObject
    {
        public List<Batch> batches = new();
        public float timeBetweenWaves;
    }
}

